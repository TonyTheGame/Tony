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


$(document).ready(function() {
	var $imagen = $("#titulo");	

	$imagen.css('margin-left', (($imagen.width() / 2) * -1) + "px");
	$imagen.css('margin-top', (($imagen.height() / 2) * -1) + "px");

	$("body").vide('video.mp4');
});

$(window).resize(function() {
	var $imagen = $("#titulo");	

	$imagen.css('margin-left', (($imagen.width() / 2) * -1) + "px");
	$imagen.css('margin-top', (($imagen.height() / 2) * -1) + "px");
});