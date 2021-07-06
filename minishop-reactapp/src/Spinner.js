import React from 'react';
import SpinnerImg from './files/spinner.mov.gif';

function Spinner(props) {
    return <div className={props.align === 'left' ? "text-left" : "text-center"}><img src={SpinnerImg} style={{ width: "20px", height: "auto" }} /> Loading</div>
}

export default Spinner;