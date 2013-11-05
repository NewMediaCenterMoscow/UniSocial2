$(function () {

	$('#OutputInDb').change(function () {

		$('#outputFilename').prop('disabled', this.checked);

	});

});