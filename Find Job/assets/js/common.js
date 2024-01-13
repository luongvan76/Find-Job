// get location
async function getLocation($scope) {
  var requestOptions = {
    method: "GET",
  };
  await fetch(
    "https://provinces.open-api.vn/api/?depth=1",
    requestOptions
  ).then(function (response) {
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
    response
      .json()
      .then(function (result) {
        $scope.locations = result; // Gán dữ liệu JSON vào $scope.locations
      })
      .catch(function (error) {
        console.log("Fetch error:", error);
      }); // Chuyển đổi phản hồi sang JSON
  });
};

// get type
function getType($http, $scope) {
  $http({
    method: "GET",
    url: url + "Type/Get-all",
  })
    .then(function (response) {
      $scope.types = response.data;
    })
    .catch(function (error) {
      console.log(error);
    });
};

// get industry
function getIndustry($http, $scope) {
  $http({
    method: "GET",
    url: url + "Industry/Get-all",
  })
    .then(function (response) {
      $scope.industries = response.data;
    })
    .catch(function (error) {
      console.log(error);
    });
}

// get all job
function GetAllJob($http, $scope, pageNumber, pageSize) {
  $http({
    method: "GET",
    url: url + "Job/FindAJob?pageNumber=" + pageNumber + "&pageSize=" + pageSize,
  })
    .then(function (response) {
      $scope.jobs = response.data.jobList;
      $scope.totalJob = response.data.jobQuantity;
      $scope.totalPages = $scope.totalJob / pageSize;
      $scope.pageNumbers = Range($scope.totalPages);
    })
    .catch(function (error) {
      console.log(error);
    });
};

 function Range (end) {
  var result = [];
  for (var i = 1; i <= end+1; i++) {
      result.push(i);
  }
  return result;
};

// get job detail
function getJobDetail($http, $scope, id) {
  $http({
    method: "GET",
    url: url + "Job/JobDetail?jobId=" + id,
  }).then(function (response) {
    $scope.job = response.data;
    CKEDITOR.instances.descriptionE.setData();
    CKEDITOR.instances.requirementE.setData();
    $scope.selectedLocation = $scope.job.location;
    $scope.deadline = parseDate($scope.job.deadline);
  })
    .catch(function (error) {
      console.log(error);
    })
};

function parseDate(inputDate) {
  var parts = inputDate.split('-');
  if (parts.length === 3) {
      var day = parseInt(parts[0]);
      var month = parseInt(parts[1]) - 1; // Giảm đi 1 vì tháng bắt đầu từ 0 trong JavaScript
      var year = parseInt(parts[2]);
      return new Date(year, month, day);
  }
  return null; // Trả về null nếu định dạng không hợp lệ
}


var searchAccount = "";
var searchJob = "";
var searchPost = "";
var searchType = "";
var searchIndustry = "";






