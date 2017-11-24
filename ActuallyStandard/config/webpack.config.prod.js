const baseConfig = require('./webpack.config.base')
const UglifyJSPlugin = require('uglifyjs-webpack-plugin')
const merge = require('webpack-merge')


module.exports = merge(baseConfig, {
    plugins: [
        new UglifyJSPlugin
    ]
})