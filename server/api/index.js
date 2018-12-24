const router = require('express').Router()
const users = require('./users')
const topics = require('./topics')

// Users routes
router.post('/users/create', users.create)

// Topics routes
router.post('/topics/create', topics.create)

module.exports = router
