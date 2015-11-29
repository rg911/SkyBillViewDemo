var app = angular.module("BillApp", []);
app.controller("BillController", ["$scope", "$http", function ($scope, $http) {
    var url = "http://safe-plains-5453.herokuapp.com/bill.json";
    //$http.get("http://localhost:55565/BillAPI/GetBillData/?url=" + encodeURIComponent(url)) //for mvc controller
    $http.get("/api/BillAPI?url=" + encodeURIComponent(url)) //url encode the url parameter
         .success(function (data, status, headers, config) {
             $scope.bill = data;
             $scope.callsLen = data.callCharges.calls.length;

         })
         .error(function (data, status, headers, config) {
             alert("error");
         });

    $scope.showMore = function () {
        $scope.end = $scope.bill.callCharges.calls.length;
    }
    $scope.showLess = function () {
        $scope.end = 6;
    }
}]);

app.filter('digits', function () {
    return function (input) {
        if (input < 10) {
            input = '0' + input;
        }

        return input;
    }
});
app.filter("formatedDate", function () {
    var re = /\\\/Date\(([0-9]*)\)\\\//;
    return function (x) {
        var m = x.match(re);
        if (m) return new Date(parseInt(m[1]));
        else return null;
    };
});
app.filter('slice', function () {
    return function (arr, start, end) {
        return arr.slice(start, end);
    };
});