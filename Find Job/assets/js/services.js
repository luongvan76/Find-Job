
app.service('HistoryService', function() {
  var service = this;
  
  service.job = false;
  service.showInforJob = function () {
      service.job = !service.job;
  }

});

app.service('ApplyService', function() {
  var service = this;
  
  service.apply = false;  
  service.showApply = function() {
    service.apply = !service.apply;
  };
});

app.service('ProfileService', function() {
  var service = this;
  
  service.Profile = true;
  service.ChangePassword = false;
  service.EditProfile = false;
  service.showProfileView = function () {
      service.Profile = true;
      service.ChangePassword = false;
      service.EditProfile = false;
  };
  service.changePassword = function () {
      service.Profile = false;
      service.ChangePassword = true;
      service.EditProfile = false;
  };

  service.showEditProfile = function () {
      service.Profile = false;
      service.ChangePassword = false;
      service.EditProfile = true;
  };
});

app.service('EmployerService', function() {
  var service = this;
  
  service.editProfile = false;  
  service.showEditProfile = function() {
    service.editProfile = !service.editProfile;
  };
  service.AllPosts = true;
  service.ApprovedPost = false;
  service.WaitingPost = false;
  service.editJob = false;
  service.inforEmployer = true;
  service.inforSeekerApply = false;
  service.apply = true;
  service.ShowAllPosts = function () {
      service.AllPosts = true;
      service.ApprovedPost = false;
      service.WaitingPost = false;
  };
  service.ShowApprovedPost = function () {
      service.AllPosts = false;
      service.ApprovedPost = true;
      service.WaitingPost = false;
  };
  service.ShowWaitingPost = function () {
      service.AllPosts = false;
      service.ApprovedPost = false;
      service.WaitingPost = true;
  };
  service.ShowEditJob = function () {
      service.editJob = !service.editJob;
  };
  service.showInforSeekerApply = function () {
      service.inforSeekerApply = true;
      service.inforEmployer = false;
  };
  service.unshowInforSeekerApply = function () {
      service.inforSeekerApply = false;
      service.inforEmployer = true;
  };
  service.showApply = function(){
    service.apply = !service.apply;
  };
});

app.service('HeaderService', function() {
  var service = this;
  service.employer = false;
  service.isUserLoggedIn = false;
  service.showEmployer = function(){
      service.employer = !service.employer;
  }
});

app.service('AdminService', function() {
  var service = this;
  service.showProfile = false;
  service.showPostJob = false;
  service.showSeeker = true;
  service.showEmployer = false;
  service.dashboard = true;
  service.account = false;
  service.job = false;
  service.post = false;
  service.type_industry = false;
  service.jobTimeout = false;
  service.InforProfile = function () {
      service.showProfile = !service.showProfile;
  };
  service.InforPostJob = function () {
      service.showPostJob = !service.showPostJob;
  };
  service.showProfileSeeker = function () {
      service.showSeeker = true;
      service.showEmployer = false;
  };
  service.showProfileEmployer = function () {
      service.showEmployer = true;
      service.showSeeker = false;
  };
// show component page
  service.showDashboard = function(){
      service.dashboard = true;
      service.account = false;
      service.job = false;
      service.post = false;
      service.type_industry = false;
      service.jobTimeout = false;
  };
  service.showAccount = function(){
      service.dashboard = false;
      service.account = true;
      service.job = false;
      service.post = false;
      service.type_industry = false;
      service.jobTimeout = false;
  };
  service.showJob = function(){
      service.dashboard = false;
      service.account = false;
      service.job = true;
      service.post = false;
      service.type_industry = false;
      service.jobTimeout = false;
  };
  service.showPost = function(){
      service.dashboard = false;
      service.account = false;
      service.job = false;
      service.post = true;
      service.type_industry = false;
      service.jobTimeout = false;
  };
  service.showTypeIndustry = function(){
      service.dashboard = false;
      service.account = false;
      service.job = false;
      service.post = false;
      service.type_industry = true;
      service.jobTimeout = false;
  };
  service.showJobTimeout = function(){
    service.dashboard = false;
    service.account = false;
    service.job = false;
    service.post = false;
    service.type_industry = false;
    service.jobTimeout = true;
  };
});

app.service('UserService', function () {
  var user = {};

 // Trong UserService
  this.setUser = function (uid, name, email, photo, phoneNumber) {
    user.Uid = uid;
    user.Name = name;
    user.Email = email;
    user.Photo = photo;
    user.PhoneNumber = phoneNumber;
  };
  this.getUser = function () {
      return user;
  };

  this.logout = function () {
      user = {};
      sessionStorage.clear();
  }
});

app.service('authService', function () {
  var token = null;

  //set Token
  this.setToken = function (newToken) {
    token = newToken;
  };

  //get Token
  this.getToken = function () {
    return token;
  };
});

app.service('notificationService', function () {
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
    toastr.success(message);
  }
  function displayError(error) {
    if(Array.isArray(error)){
      error.forEach(function (err) {
        toastr.error(err);
      })
    }
    else{
      toastr.error(error);
    }
  }
  function displayWarning(message) {
    toastr.warning(message);
  }
  function displayInfo(message) {
    toastr.info(message);
  }
});






