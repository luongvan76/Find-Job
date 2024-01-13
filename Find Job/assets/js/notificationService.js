(function (app) {
    app.factory('notificationService', notificationService);
    function notificationService(){
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "timeOut": "2000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
          };
        
          return{
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
          };
          function displaySuccess(message) {
            toastr.displaySuccess(message);
          }
          function displayError(error) {
            if(Array.isArray(error)){
              error.forEach(function (err) {
                toastr.displayError(err);
              })
            }
            else{
              toastr.displayError(error);
            }
          }
          function displayWarning(message) {
            toastr.displayWarning(message);
          }
          function displayInfo(message) {
            toastr.displayInfo(message);
          }
    }
})(angular.module('MyApp'));
  