/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./Assets/Grouping/js/profilePicker.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./Assets/Grouping/js/profilePicker.js":
/*!*********************************************!*\
  !*** ./Assets/Grouping/js/profilePicker.js ***!
  \*********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("﻿window.initializeProfilePicker = function (\n    elementId,\n    selectedItems,\n    tenantPath,\n    multiple,\n    searchPath\n) {\n    var vueMultiselect = Vue.component(\n        \"vue-multiselect\",\n        window.VueMultiselect.default\n    );\n\n    multiple = typeof multiple === \"undefined\" ? true : multiple === true;\n    searchPath = searchPath || \"ProfilePicker\";\n\n    new Vue({\n        el: \"#\" + elementId,\n        components: { \"vue-multiselect\": vueMultiselect },\n        data: {\n            value: null,\n            arrayOfItems: selectedItems,\n            options: [],\n        },\n        computed: {\n            selectedIds: function () {\n                return this.arrayOfItems\n                    .map(function (x) {\n                        return x.id;\n                    })\n                    .join(\",\");\n            },\n            isDisabled: function () {\n                return false;\n            },\n        },\n        watch: {\n            selectedIds: function () {\n                // We add a delay to allow for the <input> to get the actual value\n                // before the form is submitted\n                setTimeout(function () {\n                    $(document).trigger(\"contentpreview:render\");\n                }, 100);\n            },\n        },\n        created: function () {\n            var self = this;\n            self.asyncFind();\n        },\n        methods: {\n            asyncFind: function (query) {\n                var self = this;\n                self.isLoading = true;\n                var searchUrl = tenantPath + \"/\" + searchPath;\n\n                if (query) {\n                    searchUrl += \"?query=\" + query;\n                }\n\n                fetch(searchUrl).then(function (res) {\n                    res.json().then(function (json) {\n                        self.options = json;\n                        self.isLoading = false;\n                    });\n                });\n            },\n            onSelect: function (selectedOption, id) {\n                var self = this;\n\n                for (i = 0; i < self.arrayOfItems.length; i++) {\n                    if (self.arrayOfItems[i].id === selectedOption.id) {\n                        return;\n                    }\n                }\n\n                self.arrayOfItems.push(selectedOption);\n            },\n            remove: function (item) {\n                this.arrayOfItems.splice(this.arrayOfItems.indexOf(item), 1)\n            },\n        },\n    });\n};\n\n\n//# sourceURL=webpack:///./Assets/Grouping/js/profilePicker.js?");

/***/ })

/******/ });