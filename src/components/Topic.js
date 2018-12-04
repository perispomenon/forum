import React from 'react'
import PropTypes from 'prop-types'
import { NavLink } from 'react-router-dom'
import { format } from 'date-fns'
// import { connect } from 'react-redux'
// import autoBind from 'react-autobind'

import './Topic.css'

class Topic extends React.Component {
  render () {
    const data = this.props.data
    return (
      <div className='topicContainer'>
        <h3><NavLink to={`topic/${data.id}`}>{data.title}</NavLink></h3>
        <div>
          <p>{data.shortText}</p>
        </div>
        <div className="row">
          <div className="col-7">
            <ul className='tagsList'>
              {data.tags.map(t => <li key={t} className='badge badge-dark tagItem'>{t}</li>)}
            </ul>
          </div>
          <div className="col-4 offset-1">
            <span className='date'>{format(data.createdAt, 'MMMM DD YYYY HH:MM')}</span>
            &nbsp;&nbsp;by&nbsp;&nbsp;
            <NavLink to={`user/${data.author}`} className='author'>{data.author}</NavLink>
          </div>
        </div>
      </div>
    )
  }
}

Topic.propTypes = {
  data: PropTypes.object
}

export default Topic
