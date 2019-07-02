const path = require("path");

module.exports = {
  entry: {
    grouping: "./Assets/Grouping/js/profilePicker.js"
  },
  mode: "development",
  externals: {
    vue: "Vue"
  },
  output: {
    filename: "[name]/profilePicker.js",
    path: path.resolve(__dirname, "./wwwroot/Scripts/")
  }
};
