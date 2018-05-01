var sisrest = angular.module('Sistema Restaurante', []);

app.controller('restauranteController', ['$scope', '$http', restauranteController]);

function restauracontroller($scope, $http) {

    $http.get('http://localhost:56805/api/RestauranteAPI/Getrestaurante').success(function(data){
            $scope.restaurantelista = data;
    }).error(function () {
        $scope.erro = "Serviço indisponível, não é possível carregar a lista de restaurantes.";
    })
}