const baseConfig = require('./webpack.config.base')
const merge = require('webpack-merge')

module.exports = merge(baseConfig, {
    devtool: "source-map",
    devServer: {
        contentBase: __dirname + "/wwwroot",
        compress: true,
        port: 5555,
        overlay: true,
        proxy: {
            "/": { target: "http://localhost:5001" }
        }
    },
})