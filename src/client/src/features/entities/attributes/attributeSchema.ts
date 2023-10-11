import { YupShape } from "common/types/yupShape";
import { TFunction } from "i18next";
import * as yup from "yup";
import { AttributeFormFields } from "./AttributeForm.helpers";
import { DESCRIPTION_LENGTH, NAME_LENGTH } from "common/types/common/stringLengthConstants";
import { ConstraintType } from "common/types/attributes/constraintType";
import { XsdDataType } from "common/types/attributes/xsdDataType";

const stringValueObject = yup.object({
  value: yup.string().required(),
});

const decimalValueObject = yup.object({
  value: yup.string().matches(/^-?([0-9]*\.)?[0-9]+$/),
});

const integerValueObject = yup.object({
  value: yup.string().matches(/^-?[0-9]+$/),
});

const uriValueObject = yup.object({
  value: yup.string().required().url(),
});

export const attributeSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<AttributeFormFields>>({
    name: yup.string().max(NAME_LENGTH, t("common.validation.name.max")).required(t("common.validation.name.required")),

    description: yup.string().max(DESCRIPTION_LENGTH, t("common.validation.description.max")),

    constraintType: yup.number().when("valueConstraint", {
      is: true,
      then: (schema) => schema.required(),
    }),

    dataType: yup.number().when("valueConstraint", {
      is: true,
      then: (schema) => schema.required(),
    }),

    value: yup
      .string()
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.String,
        then: (schema) => schema.required(),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.Decimal,
        then: (schema) => schema.matches(/^-?([0-9]*\.)?[0-9]+$/),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.Integer,
        then: (schema) => schema.matches(/^-?[0-9]+$/),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.Boolean,
        then: (schema) =>
          schema
            .required()
            .test("parseCheck", "blablabla", (value) => !value || ["true", "false"].includes(value.toLowerCase())),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.AnyUri,
        then: (schema) => schema.required().url(),
      }),

    valueList: yup
      .array()
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.String,
        then: (schema) => schema.of(stringValueObject).min(2),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.Decimal,
        then: (schema) => schema.of(decimalValueObject).min(2),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.Integer,
        then: (schema) => schema.of(integerValueObject).min(2),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.AnyUri,
        then: (schema) => schema.of(uriValueObject).min(2),
      }),

    pattern: yup.string().when("constraintType", {
      is: ConstraintType.MatchesRegexPattern,
      then: (schema) => schema.required(),
    }),

    minValue: yup
      .string()
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Decimal,
        then: (schema) => schema.matches(/^(-?([0-9]*\.)?[0-9]+)?$/),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Integer,
        then: (schema) => schema.matches(/^(-?[0-9]+)?$/),
      }),

    maxValue: yup
      .string()
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Decimal,
        then: (schema) => schema.matches(/^(-?([0-9]*\.)?[0-9]+)?$/),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Integer,
        then: (schema) => schema.matches(/^(-?[0-9]+)?$/),
      })
      .when(["constraintType", "minValue"], {
        is: (ct: ConstraintType, mv: string) => ct === ConstraintType.IsInNumberRange && !mv,
        then: (schema) => schema.required(),
      })
      .when(["constraintType", "minValue"], {
        is: (ct: ConstraintType, mv: string) => ct === ConstraintType.IsInNumberRange && !!mv,
        then: (schema) => schema.test("validRangeCheck", "blablabla", (value) => !value || Number(value) > Number(yup.ref("minValue"))),
      }),
  });
