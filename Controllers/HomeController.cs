using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YtbDown.Models;
using YtbDown.Util;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YtbDown.Util;

namespace YtbDown.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly YoutubeClient _youtube;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _youtube = new YoutubeClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> BaixarArquivo(string tipoMedia, string mediaUrl)
        {
            try
            {
                var videoInformacao = await _youtube.Videos.GetAsync(mediaUrl);
                var mediaTitulo = videoInformacao.Title;
                string diretorioDeSaida = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Downloads");

                mediaTitulo = FormNome.FormatarNomeArquivo(mediaTitulo, 100);
                string extensao = tipoMedia == "audio" ? "mp3" : "mp4";
                var diretorioSaida = Path.Combine(diretorioDeSaida, $"{mediaTitulo}.{extensao}");

                Directory.CreateDirectory(diretorioDeSaida);

                var informacaoUrl = await _youtube.Videos.Streams.GetManifestAsync(mediaUrl);
                IStreamInfo infoFluxo = null;

                if (tipoMedia == "audio")
                {
                    infoFluxo = informacaoUrl.GetAudioOnlyStreams().GetWithHighestBitrate();
                }
                else if (tipoMedia == "video")
                {
                    infoFluxo = informacaoUrl.GetMuxedStreams().GetWithHighestVideoQuality();
                }

                if (infoFluxo != null)
                {
                    await _youtube.Videos.Streams.DownloadAsync(infoFluxo, diretorioSaida);
                }
                else
                {
                    throw new Exception(tipoMedia == "audio" ? "Nenhuma stream de áudio disponível." : "Nenhuma stream de vídeo disponível.");
                }

                string contentType = tipoMedia == "audio" ? "audio/mpeg" : "video/mp4";
                return PhysicalFile(diretorioSaida, contentType, Path.GetFileName(diretorioSaida));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
