$(document).ready(function() {
	var $parrafo = $("#resumen");	

	$parrafo.css('margin-left', (($parrafo.width() / 2) * -1) + "px");
	$parrafo.css('margin-top', (($parrafo.height() / 2) * -1) + "px");

	$("body").vide('video.mp4');
});

$(window).resize(function() {
	var $parrafo = $("#resumen");	

	$parrafo.css('margin-left', (($parrafo.width() / 2) * -1) + "px");
	$parrafo.css('margin-top', (($parrafo.height() / 2) * -1) + "px");
});