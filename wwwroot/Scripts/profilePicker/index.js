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

eval("﻿window.initializeProfilePicker = function (\r\n  elementId,\r\n  selectedItems,\r\n  tenantPath,\r\n  searchPath\r\n) {\r\n  var vueMultiselect = Vue.component(\r\n    \"vue-multiselect\",\r\n    window.VueMultiselect.default\r\n  );\r\n\r\n  searchPath = searchPath || 'ProfilePicker';\r\n\r\n  new Vue({\r\n    el: \"#\" + elementId,\r\n    components: { \"vue-multiselect\": vueMultiselect },\r\n    data: {\r\n      value: null,\r\n      arrayOfItems: selectedItems,\r\n      options: []\r\n    },\r\n    computed: {\r\n      selectedIds: function () {\r\n        return this.arrayOfItems\r\n          .map(function (x) {\r\n            return x.contentItemId;\r\n          })\r\n          .join(\",\");\r\n      },\r\n      isDisabled: function () {\r\n        return false;\r\n      }\r\n    },\r\n    watch: {\r\n      selectedIds: function () {\r\n        // We add a delay to allow for the <input> to get the actual value\r\n        // before the form is submitted\r\n        setTimeout(function () {\r\n          $(document).trigger(\"contentpreview:render\");\r\n        }, 100);\r\n      }\r\n    },\r\n    created: function () {\r\n      var self = this;\r\n      self.asyncFind();\r\n    },\r\n    methods: {\r\n      asyncFind: function (query) {\r\n        var self = this;\r\n        self.isLoading = true;\r\n        var searchUrl = tenantPath + '/' + searchPath;\r\n\r\n        if (query) {\r\n          searchUrl += \"&query=\" + query;\r\n        }\r\n\r\n        fetch(searchUrl).then(function (res) {\r\n          res.json().then(function (json) {\r\n            self.options = json;\r\n            self.isLoading = false;\r\n          });\r\n        });\r\n      },\r\n      onSelect: function (selectedOption, id) {\r\n        var self = this;\r\n\r\n        for (i = 0; i < self.arrayOfItems.length; i++) {\r\n          if (\r\n            self.arrayOfItems[i].contentItemId === selectedOption.contentItemId\r\n          ) {\r\n            return;\r\n          }\r\n        }\r\n\r\n        self.arrayOfItems.push(selectedOption);\r\n      },\r\n      remove: function (item) {\r\n        this.arrayOfItems.splice(this.arrayOfItems.indexOf(item), 1);\r\n      }\r\n    }\r\n  });\r\n};\r\n\n\n//# sourceURL=webpack:///./Assets/Grouping/js/profilePicker.js?");

/***/ })

/******/ });