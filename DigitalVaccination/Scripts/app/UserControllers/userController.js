var UserControllers = angular.module("UserControllers", ['ui.bootstrap', 'ng-bs3-datepicker']);


// this controller call the api method and display the list of employees
// in list.html
UserControllers.controller("UserShowController", ['$scope', '$http',
    function ($scope, $http) {
        $scope.isThingsCollapsed = false;
        $http.get('/api/User').success(function (data) {
            $scope.UserInfo = data;
            $scope.ChildsList = data.Childs;

        });

    }]
);

// this controller call the api method and display the record of selected employee
// in delete.html and provide an option for delete
UserControllers.controller("DeleteUserController", ['$scope', '$http', '$routeParams', '$location',
        function ($scope, $http, $routeParams, $location) {
            $scope.user = {};
            // $scope.id = $routeParams.id;
            $http.get('/api/User/' + $routeParams.id).success(function (data) {
                $scope.user = data;
                $scope.Id = data.Id;
                $scope.UserName = data.UserName;
                $scope.FullName = data.FullName;
                $scope.FatherName = data.FatherName;
                $scope.MotherName = data.MotherName;
                $scope.PhoneNumber = data.PhoneNumber;
                $scope.DOB = moment(data.BirthDate).format('YYYY/MM/DD');
                $scope.CurrentAddress = data.CurrentAddress;
                $scope.PermanentAddress = data.PermanentAddress;
                $scope.BirthCertificateID = data.BirthCertificateID;
                $scope.UserId = data.UserId;
            });

            $scope.ValidUser = function () {

                if ($scope.Id) { // your question said "more than one element"
                    $scope.title = "Delete User";
                    return false;

                }
                else {
                    $scope.title = "Invalid User";
                    return true;
                }
            };

            $scope.delete = function () {
                $http.delete('/api/User/' + $scope.Id).success(function (data) {
                    var url = '@Url.Action("Index", "Home")';
                    // $location.url('/User/Index');
                    location.href = "/User/Index";
                    // location.href = "/Center/Index";
                }).error(function (data) {
                    $scope.error = "An error has occured while deleting User! " + data;
                });
            };
        }
]);

// this controller call the api method and display the record of selected employee
// in edit.html and provide an option for create and modify the employee and save the employee record
UserControllers.controller("EditUserController", ['$scope', '$filter', '$http', '$routeParams', '$location',
    function ($scope, $filter, $http, $routeParams, $location) {

        $http.get('/api/UserRoles/').success(function (data) {
            $scope.roles = data;
            $scope.RoleID = data.Id;
            $scope.RoleName = data.RoleName;
            $scope.ParentRoleName = data.ParentRoleName;
        });


        $scope.DOB = $filter('date')(new Date(), 'yyyy/MM/dd');
        $scope.DatePublish = function () {
            console.log($scope.DOB);
        };
        //$scope.phoneNumbr = /^\+88?\d{2}[- ]?\d{3}[- ]?\d{5}$/;
        // $scope.phoneNumbr = /^\+88?\d{2}[- ]?\d{3}[- ]?\d{5}$/;

        $scope.phoneNumbr = /^(?:\+?88)?01[5-9]\d{8}$/;

        $scope.checkPhoneDuplicate = function () {
            console.log($scope.PhoneDuplicate + " " + $scope.PhoneNumber);
            // $scope.$watch("PhoneNumber", function (newValue, oldValue) {
            if ($scope.errorPhoneDuplicate && ($scope.PhoneNumber == $scope.PhoneDuplicate))
                return true;
            else {
                return false;
            }
            //    });

        }

        $scope.checkUserDuplicate = function () {
            console.log($scope.errorUserNameDuplicate + " " + $scope.UserName);
            // $scope.$watch("PhoneNumber", function (newValue, oldValue) {
            if ($scope.errorUserNameDuplicate && ($scope.UserName == $scope.UserNameDuplicate))
                return true;
            else {
                return false;
            }
            //    });

        }




        $scope.SubmitPermit = function () {
            console.log($scope.PhoneDuplicate + " " + $scope.PhoneNumber);
            // $scope.$watch("PhoneNumber", function (newValue, oldValue) {
            if (!$scope.UserName || !$scope.FullName || !$scope.FatherName || !$scope.MotherName || !$scope.PhoneNumber || !$scope.DOB || !$scope.CurrentAddress)
                return true;
            else {
                return false;
            }
            //    });

        }




        function errorHandle(data, exception) {
            var errorMessage = "";
            if (data.status === 0) {
                errorMessage = 'Not connect.\n Verify Network.';
            } else if (data.status == 404) {
                errorMessage = 'Requested page not found. [404]';
            } else if (data.status == 500) {
                errorMessage = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                errorMessage = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                errorMessage = 'Time out error.';
            } else if (exception === 'abort') {
                errorMessage = 'Ajax request aborted.';
            } else if (exception == 403) {
                errorMessage = "UnAuthorized Access!!!";

            }
            else {
                errorMessage = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            $scope.error = "An error has occured while Managing user!  " + errorMessage + " " + exception;
        }
        $scope.isDate = false;
        $scope.ID = 0;
        $scope.save = function () {
            var obj = {
                BirthCertificateID: $scope.BirthCertificateID,
                BirthDate: $scope.DOB,
                CurrentAddress: $scope.CurrentAddress,
                FatherName: $scope.FatherName,
                FullName: $scope.FullName,
                Id: $scope.ID,
                MotherName: $scope.MotherName,
                PermanentAddress: $scope.PermanentAddress,
                PhoneNumber: $scope.PhoneNumber,
                UserId: $scope.UserId,
                UserName: $scope.UserName,
                UserRole: $scope.RoleID
            };
            if ($scope.ID == 0) {

                $http.post('/api/User/', obj).success(function (data) {
                    location.href = "/User/Index";
                }).error(function (xhr) {
                    if (xhr.Message == "Duplicate Phone Number") {
                        $scope.errorPhoneDuplicate = xhr.Message;
                        $scope.PhoneDuplicate = $scope.PhoneNumber;
                    } else if (xhr.Message == "Duplicate UserName") {

                        $scope.errorUserNameDuplicate = xhr.Message;
                        $scope.UserNameDuplicate = $scope.UserName;
                    }
                });
            }
            else {
                $http.put('/api/User/', obj).success(function (data) {
                    location.href = "/User/Index";
                }).error(function (data, xhr) {
                    $scope.error = "An Error has occured while Managing user! " + data.status + " " + xhr;
                });
            }
        }

        if ($routeParams.id) {
            $scope.form = {};

            $http.get('/api/User/' + $routeParams.id).success(function (data) {
                $scope.user = data;
                $scope.ID = data.Id;
                $scope.UserName = data.UserName;
                $scope.FullName = data.FullName;
                $scope.FatherName = data.FatherName;
                $scope.MotherName = data.MotherName;
                $scope.PhoneNumber = data.PhoneNumber;
                $scope.DOB = moment(data.BirthDate).format('YYYY/MM/DD');
                $scope.CurrentAddress = data.CurrentAddress;
                $scope.PermanentAddress = data.PermanentAddress;
                $scope.BirthCertificateID = data.BirthCertificateID;
                $scope.UserId = data.UserId;
                $scope.RoleID = data.UserRole;

                console.log($scope.UserSpecificRoleId);
                if ($scope.ID) {
                    $scope.title = "Edit User";
                } else {
                    $scope.title = "Create New User";
                    console.log($scope.ID);
                    //location.href = "/User/Index";
                }
            }).error(function (data, exception) {
                errorHandle(data, exception);
            });

        }
        else {
            $scope.title = "Create New User";
        }
    }
]);
