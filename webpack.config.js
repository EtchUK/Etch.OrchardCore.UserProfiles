const path = require("path");

module.exports = {
  entry: {
    groupfield: "./Assets/GroupField/js/profileGroupPicker.js",
    grouping: "./Assets/Grouping/js/profilePicker.js"
  },
  mode: "development",
  externals: {
    vue: "Vue"
  },
  output: {
    filename: "[name]/index.js",
    path: path.resolve(__dirname, "./wwwroot/Scripts/")
  }
};
