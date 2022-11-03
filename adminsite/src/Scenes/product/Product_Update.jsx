import Modal from 'react-bootstrap/Modal';

import { Box, Button, TextField } from "@mui/material"
import MenuItem from '@mui/material/MenuItem';
import { Formik } from "formik"
import { useState, useEffect } from "react"
import * as Yup from 'yup';
import useMediaQuery from "@mui/material/useMediaQuery"
import Header from "../../components/Header"
import axios from 'axios'

function ProductUpdateForm() {
    const [category, setCategory] = useState([])
    const isNoneMobile = useMediaQuery("(min-width:600px)");
    useEffect(() => {
        axios.get(`https://localhost:7173/api/Category`)
        .then(res => {
            setCategory(res.data)
        }).catch(error => console.log(error))
    }, [])
    const categorySelection = category.reduce((init, curValue) => [...init, {value:curValue.id, label:curValue.name}], [])


    const initialValues = {
        productImg : props.data.productImg,
        productName : props.data.productName,
        description : props.data.description,
        categoryId : props.data.categoryId,
        price : props.data.price,
        quantity: props.data.quantity,
        inventoryNumber: props.data.inventoryNumber
    }

    const handleUpdate = async () => {
        await axios.post(`https://localhost:7173/Product/UpdateProduct/${props.data.id}`)
    }

    const handleClose = () => {
        props.setUpdate(!props.show)
    }

    return (
        <Modal show={props.show} onHide={props.setUpdate}>
            <Modal.Header closeButton>
                <Modal.Title>Update product</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                Some content
            </Modal.Body>
            <Modal.Footer>
            </Modal.Footer>
        </Modal>
    );
}

export default ProductUpdateForm