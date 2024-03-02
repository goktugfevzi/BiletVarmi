import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import dayjs from "dayjs";
import "./biletVarmi.css";
import { FaArrowLeft } from "react-icons/fa";
import { useNavigate } from "react-router-dom";

const validationSchema = Yup.object({
  fromTrain: Yup.string().required("Nereden bilgisi gerekli"),
  toTrain: Yup.string().required("Nereye bilgisi gerekli"),
    email: Yup.string().email("Geçersiz e-mail adresi").required("Zorunlu alan"),
    departureDate: Yup.string().required("Tarih Alanı Zorunlu (ör: 18.02.2024"),
  startTime: Yup.string()
    .required("Başlangıç saati gerekli (ör: 12:30)")
    .matches(/^([0-1]?[0-9]|2[0-3]):([0-5][0-9])$/, {
      message: "Geçersiz saat formatı (ör: 12:30)",
    }),
  endTime: Yup.string()
    .required("Bitiş saati gerekli (ör: 12:30)")
    .matches(/^([0-1]?[0-9]|2[0-3]):([0-5][0-9])$/, {
      message: "Geçersiz saat formatı (ör: 12:30)",
    }),
});

const initialValues = {
  fromTrain: "",
  toTrain: "",
  departureDate: dayjs().add(0, "day").format("YYYY-MM-DD"),
  email: "",
  startTime: dayjs().format("HH:mm"),
  endTime: "",
};

const BiletVarmi = () => {
  const navigate = useNavigate();
  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={async (values) => {
        values.departureDate = dayjs(values.departureDate).format("DD.MM.YYYY");
        console.log("Submitted values:", values);
          await fetch("https://localhost:7145/api/SystemRunner/calistirTest", {
          method: "POST",
          body: JSON.stringify(values),
          headers: { "Content-Type": "application/json" },
        }).then((r) => console.log(r));
      }}
    >
      {({ errors, touched }) => (
        <Form className="form">
          <div className="form-group-time d-flex justify-content-between">
            <label>TCDD Sitesindeki gibi yaz ör:(Ankara Gar, Eskişehir)</label>
            <button
              className="arrow-Left"
              onClick={() => navigate("/")}
            >
              <FaArrowLeft />
            </button>
          </div>

          <div className="form-group">
            <label htmlFor="fromTrain">Nerden</label>
            <Field
              type="text"
              name="fromTrain"
              placeholder="Nerden"
              className={`form-control ${
                errors.fromTrain && touched.fromTrain ? "is-invalid" : ""
              }`}
            />
            <ErrorMessage
              name="fromTrain"
              component="div"
              className="invalid-feedback"
            />
          </div>
          <div className="form-group">
            <label htmlFor="toTrain">Nereye</label>
            <Field
              type="text"
              name="toTrain"
              placeholder="Nereye"
              className={`form-control ${
                errors.toTrain && touched.toTrain ? "is-invalid" : ""
              }`}
            />
            <ErrorMessage
              name="toTrain"
              component="div"
              className="invalid-feedback"
            />
          </div>
          <div className="form-group">
            <label htmlFor="email">E-mail</label>
            <Field
              type="email"
              name="email"
              placeholder="E-mail"
              className={`form-control ${
                errors.email && touched.email ? "is-invalid" : ""
              }`}
            />
            <ErrorMessage
              name="email"
              component="div"
              className="invalid-feedback"
            />
          </div>
          <div className="form-group">
            <label htmlFor="departureDate">Yolculuk Günü</label>
            <Field
              type="date"
              name="departureDate"
              className={`form-control ${
                errors.departureDate && touched.departureDate
                  ? "is-invalid"
                  : ""
              }`}
            />
            <ErrorMessage
              name="departureDate"
              component="div"
              className="invalid-feedback"
            />
          </div>
          <label>Hangi Saatler Arasında Yolculuk Yapacaksın</label>
          <div className="form-group-time d-flex justify-content-between">
            <div className="form-group-time-field">
              <label htmlFor="startTime"> Başlangıç Saati</label>
              <Field
                type="time"
                name="startTime"
                className={`form-control ${
                  errors.startTime && touched.startTime ? "is-invalid" : ""
                }`}
              />
              <ErrorMessage
                name="startTime"
                component="div"
                className="invalid-feedback"
              />
            </div>
            <div className="form-group-time-field">
              <label htmlFor="endTime"> Bitiş Saati</label>
              <Field
                type="time"
                name="endTime"
                className={`form-control ${
                  errors.endTime && touched.endTime ? "is-invalid" : ""
                }`}
              />
              <ErrorMessage
                name="endTime"
                component="div"
                className="invalid-feedback"
              />
            </div>
          </div>

          <button type="submit" style={{fontSize:16}} className="btn btn-primary btn-lg btn-block">
            Bilet Bulmaya Başlasın
          </button>
        </Form>
      )}
    </Formik>
  );
};

export default BiletVarmi;
