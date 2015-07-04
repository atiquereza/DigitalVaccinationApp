/**
 * Created by Rijwan on 5/31/2015.
 */

//DatePicker function
$(function () {

    $('#datetimepicker1').datepicker({
        locale:'en',
        todayHighlight: true,
        format: 'dd/mm/yyyy'
    }).on('changeDate', function(e){
        $(this).datepicker('hide');
});
})


//$(document).ready(function () {
//
//    $('#datepicker1').datepicker({
//        format: "dd/mm/yyyy"
//    });
//
//});
