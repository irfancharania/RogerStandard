const ExtractTextPlugin = require("extract-text-webpack-plugin");

module.exports = {
    entry: [
        './Assets/src/index.tsx',
        './Assets/sass/index.scss',
        './node_modules/bootstrap/dist/css/bootstrap.min.css'
    ],
    output: {
        filename: 'bundle.js',
        path: __dirname + '/wwwroot/dist'
    },

    resolve: {
        extensions: [".ts", ".tsx", ".js", ".json", ".css", ".scss"],
    },


    module: {
        rules: [
            { test: /\.tsx?$/, loader: "awesome-typescript-loader" },

            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { enforce: "pre", test: /\.js$/, loader: "source-map-loader" },

            {
                test: /\.scss$/,
                use: ExtractTextPlugin.extract({
                    fallback: 'style-loader',
                    use: ['css-loader', 'sass-loader']
                })
            },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: 'style-loader',
                    use: 'css-loader'
                })
            }
        ]

    },
    plugins: [
        new ExtractTextPlugin("styles.css"),
    ]
};