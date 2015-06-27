function PopUpWindow(url) {
    newwindow = window.open(url, 'name');
    if (window.focus) {newwindow.focus(); }
    return false;
}

function GetBaseUrl() {
    var url = window.location.href.split('/');
    var baseUrl = url;
    if (url[3] == "ManiacProject" || url[3] == "omcb") {
        baseUrl = url[0] + '//' + url[2] + '/' + url[3] + '/';
    }else {
        baseUrl = url[0] + '//' + url[2] + '/';
    }
    return baseUrl + "omcb/";
    //return "http://nmc-12test/nmc/";
}

function OnClickDelete() {
   var r = confirm("Are you sure to delete the entry");
    if (r != true)
        return false;

}