import { TFunction } from "i18next";
import * as yup from "yup";
import { DESCRIPTION_LENGTH, NAME_LENGTH, VALUE_LENGTH } from "common/types/common/stringLengthConstants";
import { ConstraintType } from "common/types/attributes/constraintType";
import { XsdDataType } from "common/types/attributes/xsdDataType";

const stringValueObject = (t: TFunction<"translation">) =>
  yup.object({
    value: yup
      .string()
      .required(t("attribute.validation.common.string"))
      .max(VALUE_LENGTH, t("attribute.validation.value.max", { length: VALUE_LENGTH })),
  });

const decimalValueObject = (t: TFunction<"translation">) =>
  yup.object({
    value: yup
      .string()
      .matches(/^-?([0-9]*\.)?[0-9]+$/, t("attribute.validation.common.decimal"))
      .max(VALUE_LENGTH, t("attribute.validation.value.max", { length: VALUE_LENGTH })),
  });

const integerValueObject = (t: TFunction<"translation">) =>
  yup.object({
    value: yup
      .string()
      .matches(/^-?[0-9]+$/, t("attribute.validation.common.integer"))
      .max(VALUE_LENGTH, t("attribute.validation.value.max", { length: VALUE_LENGTH })),
  });

const uriValueObject = (t: TFunction<"translation">) =>
  yup.object({
    value: yup
      .string()
      .required(t("attribute.validation.common.iri"))
      .url(t("attribute.validation.common.iri"))
      .max(VALUE_LENGTH, t("attribute.validation.value.max", { length: VALUE_LENGTH })),
  });

export const attributeSchema = (t: TFunction<"translation">) =>
  yup.object({
    name: yup
      .string()
      .max(NAME_LENGTH, t("common.validation.name.max", { length: NAME_LENGTH }))
      .required(t("common.validation.name.required")),

    description: yup
      .string()
      .max(DESCRIPTION_LENGTH, t("common.validation.description.max", { length: DESCRIPTION_LENGTH })),

    units: yup.array().required(),

    unitRequirement: yup.number().required(),

    valueConstraint: yup.boolean().required(),

    constraintType: yup.number().when("valueConstraint", {
      is: true,
      then: (schema) => schema.required(t("attribute.validation.constraintType")),
    }),

    dataType: yup.number().when("valueConstraint", {
      is: true,
      then: (schema) => schema.required(t("attribute.validation.dataType")),
    }),

    requireValue: yup.boolean().required(),

    value: yup
      .string()
      .max(VALUE_LENGTH, t("attribute.validation.value.max", { length: VALUE_LENGTH }))
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.String,
        then: (schema) => schema.required(t("attribute.validation.common.string")),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.Decimal,
        then: (schema) => schema.matches(/^-?([0-9]*\.)?[0-9]+$/, t("attribute.validation.common.decimal")),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.Integer,
        then: (schema) => schema.matches(/^-?[0-9]+$/, t("attribute.validation.common.integer")),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.Boolean,
        then: (schema) =>
          schema
            .required(t("attribute.validation.value.boolean"))
            .test(
              "validBooleanCheck",
              t("attribute.validation.value.boolean"),
              (value) => !value || ["true", "false"].includes(value.toLowerCase()),
            ),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.HasSpecificValue && dt === XsdDataType.AnyUri,
        then: (schema) =>
          schema.required(t("attribute.validation.common.iri")).url(t("attribute.validation.common.iri")),
      }),

    valueList: yup
      .array()
      .required()
      .when("constraintType", {
        is: ConstraintType.IsInListOfAllowedValues,
        then: (schema) => schema.min(2, t("attribute.validation.valueList.min")),
      })
      .test("uniqueValues", t("attribute.validation.valueList.unique"), (value, context) => {
        if (context.parent.dataType === XsdDataType.Decimal || context.parent.dataType === XsdDataType.Integer) {
          return value ? value.length === new Set(value.map((x) => Number(x.value))).size : true;
        }
        return value ? value.length === new Set(value.map((x) => x.value)).size : true;
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.String,
        then: (schema) => schema.of(stringValueObject(t)),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.Decimal,
        then: (schema) => schema.of(decimalValueObject(t)),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.Integer,
        then: (schema) => schema.of(integerValueObject(t)),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInListOfAllowedValues && dt === XsdDataType.AnyUri,
        then: (schema) => schema.of(uriValueObject(t)),
      }),

    pattern: yup
      .string()
      .max(VALUE_LENGTH, t("attribute.validation.value.max", { length: VALUE_LENGTH }))
      .when("constraintType", {
        is: ConstraintType.MatchesRegexPattern,
        then: (schema) => schema.required(t("attribute.validation.pattern")),
      }),

    minValue: yup
      .string()
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Decimal,
        then: (schema) => schema.matches(/^(-?([0-9]*\.)?[0-9]+)?$/, t("attribute.validation.common.decimal")),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Integer,
        then: (schema) => schema.matches(/^(-?[0-9]+)?$/, t("attribute.validation.common.integer")),
      }),

    maxValue: yup
      .string()
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Decimal,
        then: (schema) => schema.matches(/^(-?([0-9]*\.)?[0-9]+)?$/, t("attribute.validation.common.decimal")),
      })
      .when(["constraintType", "dataType"], {
        is: (ct: ConstraintType, dt: XsdDataType) =>
          ct === ConstraintType.IsInNumberRange && dt === XsdDataType.Integer,
        then: (schema) => schema.matches(/^(-?[0-9]+)?$/, t("attribute.validation.common.integer")),
      })
      .when(["constraintType", "minValue"], {
        is: (ct: ConstraintType, mv: string) => ct === ConstraintType.IsInNumberRange && !mv,
        then: (schema) => schema.required(t("attribute.validation.range.empty")),
      })
      .when(["constraintType", "minValue"], {
        is: (ct: ConstraintType, mv: string) => ct === ConstraintType.IsInNumberRange && !!mv,
        then: (schema) =>
          schema.test(
            "validRangeCheck",
            t("attribute.validation.range.invalid"),
            (value, context) => !value || Number(value) > Number(context.parent.minValue),
          ),
      }),
  });
