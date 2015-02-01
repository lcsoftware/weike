

var studyprocessModule = angular.module('app.mooc', []);
studyprocessModule.controller('StudyProcessController', ['$scope', '$state', 'studyprocessProviderUrl', function ($scope, $state, moocProviderUrl) {

    $scope.OCID = 1; //课程编号
    $scope.OCMoocClassList = null
    $scope.key = "";
    $scope.PageIndex = 1;
    $scope.PageSize = 2;
    $scope.PageSum = 0;
    $scope.count = 0;   //判断是否是首次加载

    var OCMoocClass_List = function () { //绑定MOOC基本信息
        var url = studyprocessProviderUrl + "/OCMoocClass_List";
        var param = { OCID: $scope.OCID, Key: $scope.key, IsHistroy: -1, PageIndex: $scope.PageIndex, PageSize: $scope.PageSize }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                alert(data.d);
            }
        });
    }


    

    

    //$scope.OCMoocClass_List = function () {
       
    //    var url = moocProviderUrl + "/File_Search";
    //    var param = { file: FileMode, PageSize: $scope.PageSize, PageIndex: $scope.PageIndex }
    //    var fileList = $scope.baseService.postPromise(url, param);
    //    $scope.baseService.runPromises({
    //        fileList: fileList
    //    }, function (data) {
    //        if (data.fileList.d != null) {
    //            $scope.OCFileList = data.fileList.d;
    //            $scope.PageSum = Math.ceil($scope.OCFileList[0].RowsCount / $scope.PageSize);
    //            if ($scope.count == 0) {        //分页控件只加载一次
    //                FilePage($scope.PageSum);
    //            }
    //        }
    //    });
    //}
    //var FilePage = function (PageSum) {
    //    laypage({
    //        cont: $('#page'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
    //        pages: PageSum, //总页数
    //        skip: true, //是否开启跳页
    //        skin: '#AF0000', //选中的颜色
    //        groups: 5,//连续显示分页数
    //        first: false, //若不显示，设置false即可  
    //        last: false, //若不显示，设置false即可
    //        jump: function (e) { //触发分页后的回调
    //            $scope.PageIndex = e.curr;
    //            if ($scope.count > 0) {
    //                $scope.OCFile_List();
    //            }
    //            $scope.count++;
    //        }
    //    });
    //}
    //$scope.OCFile_List();
}]);