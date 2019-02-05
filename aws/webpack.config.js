// Import path for resolving file paths
const path = require('path');

module.exports = {
    target: 'node', // in order to ignore built-in modules like path, fs, etc.
    mode: 'production',
    externals: {
        'aws-sdk': 'aws-sdk'
    },

    // Specify the entry point for our lambda's.
    // entry: entry(entry.basePath('src'), 'src/**/*.lambda.ts'),
    entry: {
        'request-car-entry': './src/infra/api/request-car-entry.lambda.ts',
        'confirm-appointment-adapter': './src/infra/adapters/confirm-appointment-adapter.lambda.ts',
        'confirm-employee-adapter': './src/infra/adapters/confirm-employee-adapter.lambda.ts',
        'find-matching-license-plate-adapter': './src/infra/adapters/find-matching-license-plate-adapter.lambda.ts',
        'contact-notification-adapter': './src/infra/adapters/contact-notification-adapter.lambda.ts',
        'garage-gateway-adapter': './src/infra/adapters/garage-gateway-adapter.lambda.ts'
    },

    resolve: {
        extensions: ['.ts', '.tsx', '.js', '.jsx']
    },

    // Specify the output file containing our bundled code
    output: {
        path: path.join(__dirname, "./dist/"),
        library: "[name]",
        libraryTarget: "commonjs2",
        filename: "[name].js"
    },
    module: {
        rules: [
            {
                test: /\.json$/,
                exclude: /node_modules/,
                loaders: ['json']
            },
            {
                test: /\.tsx?$/,
                exclude: /tests/,
                loader: 'ts-loader',
                options: {
                    configFile: 'tsconfig.json'
                }
            }
        ]
    }
};