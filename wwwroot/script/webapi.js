

var baseUri = window.location.protocol + "//" + window.location.host + "/api/";



function GetLatestTemperature() {
   jQuery.support.cors = true;
  
   return $.ajax({
      url: baseUri + 'TemperatureReading/GetLatest',
      type: 'GET',
      dataType: 'json'
   });
}

function GetMaxTemperature(dateForMax) {
   jQuery.support.cors = true;

   return $.ajax({
      url: baseUri + 'TemperatureReading/GetMax?dateForMax=' + dateForMax,
      type: 'GET',
      dataType: 'json'
   });
}

function GetMinTemperature(dateForMax) {
   jQuery.support.cors = true;

   return $.ajax({
      url: baseUri + 'TemperatureReading/GetMin?dateForMin=' + dateForMax,
      type: 'GET',
      dataType: 'json'
   });
}

function GetFindTemperaturesByDate(startdate, enddate) {
   jQuery.support.cors = true;
   var uri = baseUri + 'TemperatureReading/Search?startdate=' + startdate + '&enddate=' + enddate;
   return $.ajax({
      url: uri,
      type: 'GET',
      dataType: 'json'
   });
}
