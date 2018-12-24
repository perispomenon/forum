const express = require('express')
const bodyParser = require('body-parser')
const compression = require('compression')
const morgan = require('morgan')
require('./db')

const apiRouter = require('./api/')

const router = express.Router()
const app = express()

app.use(compression())
app.use(morgan('dev'))
app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json())

app.use('/', router)
router.use('api/', apiRouter)

app.listen('3333', err => {
  if (err) {
    console.error(err)
  } else {
    console.log('server started')
  }
})
