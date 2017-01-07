var app = angular.module('app', []);
app.controller('ctrl', ['$scope','$http', function($scope, $http){
	$scope.items = [];
	$scope.load = function()
	{
		$http.get('http://localhost:53193/api/references')
			 .then(function(response){
			 	$scope.items = response.data;			 	
			 });

	};
	$scope.add = function(value)
	{
		$http.post('http://localhost:53193/api/references', { name : value.name })
			 .then(function(response){
			 	$scope.items.push(response.data);			 	
			 });
	}
	$scope.update = function(value)
	{
		$http.put('http://localhost:53193/api/references/' + value.id, { id: value.id, name : value.name })
			 .then(function(response){
			 	console.log(response);			 	
			 });
	}
	$scope.load();
}]);