angular.module("umbraco").controller("Limbo.Umbraco.Redirects.ProcessorsController", function ($scope, $http, editorService) {

    const vm = this;

    vm.availableProcessors = [];
    vm.selectedProcessors = [];

    vm.sortableOptions = {
        distance: 10,
        tolerance: "pointer",
        scroll: true,
        zIndex: 6000,
        disabled: false,
        containment: "parent",
        stop: function () {
            sync();
        }
    };

    function hi(x) {
        return {
            alias: x.type,
            name: x.name,
            description: x.description,
            icon: x.icon ?? "icon-binarycode"
        };
    }

    vm.add = function() {

        editorService.itemPicker({
            selectedItems: vm.selectedProcessors,
            availableItems: vm.availableProcessors,
            filter: false,
            submit: function (model) {
                vm.selectedProcessors.push(model.selectedItem);
                sync();
                editorService.close();
            },
            close: function () {
                editorService.close();
            }
        });

    };

    vm.remove = function(index) {
        vm.selectedProcessors.splice(index, 1);
        sync();
    };

    function sync() {

        const temp = [];

        vm.selectedProcessors.forEach(function(p) {
            temp.push(p.alias);
        });

        $scope.model.value = temp;

    }

    function init() {

        vm.loading = true;

        $http.get(`${Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath}/backoffice/Limbo/RichText/GetProcessors`).then(function(r) {

            vm.loading = false;

            vm.availableProcessors = r.data.map(hi);

            if (!Array.isArray($scope.model.value) || $scope.model.length === 0) return;

            $scope.model.value.forEach(function (alias) {

                // Earlier versions checked the type's assembly qualified name, which unfortutanely also contains the
                // version number, which resulted in the saved processors getting when upgrading
                alias = alias.split(",").slice(0, 2).join(",");

                const p = vm.availableProcessors.find(x => x.alias === alias);

                if (p) {
                    vm.selectedProcessors.push(p);
                } else {
                    vm.selectedProcessors.push({
                        alias: alias,
                        name: "Processor not found",
                        description: "",
                        icon: "icon-binarycode color-grey"
                    });
                }

            });

        });

    }

    init();

});