window.initializeProfilePicker = function (
  elementId,
  selectedItems,
  tenantPath,
  multiple,
  searchPath
) {
  var vueMultiselect = Vue.component(
    "vue-multiselect",
    window.VueMultiselect.default
  );

  multiple = typeof(multiple) === 'undefined' ? true : multiple == true;
  searchPath = searchPath || 'ProfilePicker';

  new Vue({
    el: "#" + elementId,
    components: { "vue-multiselect": vueMultiselect },
    data: {
      value: null,
      arrayOfItems: selectedItems,
      options: []
    },
    computed: {
      selectedIds: function () {
        return this.arrayOfItems
          .map(function (x) {
            return x.contentItemId;
          })
          .join(",");
      },
      isDisabled: function () {
        return false;
      }
    },
    watch: {
      selectedIds: function () {
        // We add a delay to allow for the <input> to get the actual value
        // before the form is submitted
        setTimeout(function () {
          $(document).trigger("contentpreview:render");
        }, 100);
      }
    },
    created: function () {
      var self = this;
      self.asyncFind();
    },
    methods: {
      asyncFind: function (query) {
        var self = this;
        self.isLoading = true;
        var searchUrl = tenantPath + '/' + searchPath;

        if (query) {
          searchUrl += "?query=" + query;
        }

        fetch(searchUrl).then(function (res) {
          res.json().then(function (json) {
            self.options = json;
            self.isLoading = false;
          });
        });
      },
      onSelect: function (selectedOption, id) {
        var self = this;

        if (!multiple && self.arrayOfItems.length > 0) {
          return;
        }

        for (i = 0; i < self.arrayOfItems.length; i++) {
          if (
            self.arrayOfItems[i].contentItemId === selectedOption.contentItemId
          ) {
            return;
          }
        }

        self.arrayOfItems.push(selectedOption);
      },
      remove: function (item) {
        this.arrayOfItems.splice(this.arrayOfItems.indexOf(item), 1);
      }
    }
  });
};
