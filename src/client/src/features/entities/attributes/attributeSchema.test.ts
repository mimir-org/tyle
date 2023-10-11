import { DESCRIPTION_LENGTH, NAME_LENGTH } from "common/types/common/stringLengthConstants";
import { AttributeFormFields } from "./AttributeForm.helpers";
import { attributeSchema } from "./attributeSchema";
import { vi } from "vitest";
import { ConstraintType } from "common/types/attributes/constraintType";
import { XsdDataType } from "common/types/attributes/xsdDataType";

describe("attributeSchema tests", () => {
  const t = vi.fn();

  it("should resolve with a name", async () => {
    const attributeWithoutName: Partial<AttributeFormFields> = { name: "Test name" };
    await expect(attributeSchema(t).validateAt("name", attributeWithoutName)).resolves.toBeTruthy();
  });

  it("should reject without a name", async () => {
    const attributeWithoutName: Partial<AttributeFormFields> = { name: "" };
    await expect(attributeSchema(t).validateAt("name", attributeWithoutName)).rejects.toBeTruthy();
  });

  it("should resolve with a name with the limit length", async () => {
    const attributeWithLongName: Partial<AttributeFormFields> = { name: "c".repeat(NAME_LENGTH) };
    await expect(attributeSchema(t).validateAt("name", attributeWithLongName)).resolves.toBeTruthy();
  });

  it("should reject with a name longer than the limit", async () => {
    const attributeWithLongName: Partial<AttributeFormFields> = { name: "c".repeat(NAME_LENGTH + 1) };
    await expect(attributeSchema(t).validateAt("name", attributeWithLongName)).rejects.toBeTruthy();
  });

  it("should resolve with a description with the limit length", async () => {
    const attributeWithLongDescription: Partial<AttributeFormFields> = {
      description: "c".repeat(DESCRIPTION_LENGTH),
    };
    await expect(attributeSchema(t).validateAt("description", attributeWithLongDescription)).resolves.toBeTruthy();
  });

  it("should reject with a description longer than the limit", async () => {
    const attributeWithLongDescription: Partial<AttributeFormFields> = {
      description: "c".repeat(DESCRIPTION_LENGTH + 1),
    };
    await expect(attributeSchema(t).validateAt("description", attributeWithLongDescription)).rejects.toBeTruthy();
  });

  it("should reject when constraint type or data type is not set for a value constraint", async () => {
    const attributeWithConstraintMissingMandatoryFields: Partial<AttributeFormFields> = {
      valueConstraint: true,
    };
    await expect(
      attributeSchema(t).validateAt("constraintType", attributeWithConstraintMissingMandatoryFields),
    ).rejects.toBeTruthy();
    await expect(
      attributeSchema(t).validateAt("dataType", attributeWithConstraintMissingMandatoryFields),
    ).rejects.toBeTruthy();
  });

  it("should resolve when constraint type is set for a value constraint", async () => {
    const attributeWithConstraintAndConstraintType: Partial<AttributeFormFields> = {
      valueConstraint: true,
      constraintType: ConstraintType.HasSpecificDataType,
    };
    await expect(
      attributeSchema(t).validateAt("constraintType", attributeWithConstraintAndConstraintType),
    ).resolves.toBeTruthy();
  });

  it("should resolve when data type is set for a value constraint", async () => {
    const attributeWithConstraintAndDataType: Partial<AttributeFormFields> = {
      valueConstraint: true,
      dataType: XsdDataType.AnyUri,
    };
    await expect(attributeSchema(t).validateAt("dataType", attributeWithConstraintAndDataType)).resolves.toBeTruthy();
  });

  it(`should resolve for valid input to ${ConstraintType[ConstraintType.HasSpecificValue]} constraint`, async () => {
    const stringValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.String,
      value: "Test value",
    };
    await expect(attributeSchema(t).validateAt("value", stringValueConstraint)).resolves.toBeTruthy();
    const decimalValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.Decimal,
      value: "12.3",
    };
    await expect(attributeSchema(t).validateAt("value", decimalValueConstraint)).resolves.toBeTruthy();
    const intValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.Integer,
      value: "-432",
    };
    await expect(attributeSchema(t).validateAt("value", intValueConstraint)).resolves.toBeTruthy();
    const boolValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.Boolean,
      value: "true",
    };
    await expect(attributeSchema(t).validateAt("value", boolValueConstraint)).resolves.toBeTruthy();
    const iriValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.AnyUri,
      value: "http://example.com/example/unit",
    };
    await expect(attributeSchema(t).validateAt("value", iriValueConstraint)).resolves.toBeTruthy();
  });

  it(`should reject for invalid input to ${ConstraintType[ConstraintType.HasSpecificValue]} constraint`, async () => {
    const stringValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.String,
      value: "",
    };
    await expect(attributeSchema(t).validateAt("value", stringValueConstraint)).rejects.toBeTruthy();
    const decimalValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.Decimal,
      value: "12.3M",
    };
    await expect(attributeSchema(t).validateAt("value", decimalValueConstraint)).rejects.toBeTruthy();
    const intValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.Integer,
      value: "-432.45",
    };
    await expect(attributeSchema(t).validateAt("value", intValueConstraint)).rejects.toBeTruthy();
    const boolValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.Boolean,
      value: "maybe",
    };
    await expect(attributeSchema(t).validateAt("value", boolValueConstraint)).rejects.toBeTruthy();
    const iriValueConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.HasSpecificValue,
      dataType: XsdDataType.AnyUri,
      value: "example.com/example/unit",
    };
    await expect(attributeSchema(t).validateAt("value", iriValueConstraint)).rejects.toBeTruthy();
  });

  it(`should resolve for valid input to ${
    ConstraintType[ConstraintType.IsInListOfAllowedValues]
  } constraint`, async () => {
    const stringValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.String,
      valueList: [{ value: "Test value 1" }, { value: "Test value 2" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", stringValueListConstraint)).resolves.toBeTruthy();
    const decimalValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.Decimal,
      valueList: [{ value: "12.3" }, { value: "-45" }, { value: ".23" }, { value: "0" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", decimalValueListConstraint)).resolves.toBeTruthy();
    const intValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.Integer,
      valueList: [{ value: "12" }, { value: "-45" }, { value: "0" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", intValueListConstraint)).resolves.toBeTruthy();
    const iriValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.AnyUri,
      valueList: [{ value: "http://example.com/weight" }, { value: "http://www.test.com" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", iriValueListConstraint)).resolves.toBeTruthy();
  });

  it(`should reject for invalid input to ${
    ConstraintType[ConstraintType.IsInListOfAllowedValues]
  } constraint`, async () => {
    const shortValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.String,
      valueList: [{ value: "Test value 1" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", shortValueListConstraint)).rejects.toBeTruthy();
    const emptyValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.Integer,
      valueList: [],
    };
    await expect(attributeSchema(t).validateAt("valueList", emptyValueListConstraint)).rejects.toBeTruthy();
    const stringValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.String,
      valueList: [{ value: "Test value 1" }, { value: "" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", stringValueListConstraint)).rejects.toBeTruthy();
    const decimalValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.Decimal,
      valueList: [{ value: "12.3" }, { value: "-45" }, { value: "X.23" }, { value: "0" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", decimalValueListConstraint)).rejects.toBeTruthy();
    const intValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.Integer,
      valueList: [{ value: "12" }, { value: "-45" }, { value: "null" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", intValueListConstraint)).rejects.toBeTruthy();
    const iriValueListConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInListOfAllowedValues,
      dataType: XsdDataType.AnyUri,
      valueList: [{ value: "http://example.com/weight" }, { value: "www.test.com" }],
    };
    await expect(attributeSchema(t).validateAt("valueList", iriValueListConstraint)).rejects.toBeTruthy();
  });

  it(`should resolve for valid input to ${ConstraintType[ConstraintType.MatchesRegexPattern]} constraint`, async () => {
    const patternConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.MatchesRegexPattern,
      dataType: XsdDataType.String,
      pattern: "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$",
    };
    await expect(attributeSchema(t).validateAt("pattern", patternConstraint)).resolves.toBeTruthy();
  });

  it(`should reject for invalid input to ${
    ConstraintType[ConstraintType.MatchesRegexPattern]
  } constraint`, async () => {
    const patternConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.MatchesRegexPattern,
      dataType: XsdDataType.String,
      pattern: "",
    };
    await expect(attributeSchema(t).validateAt("pattern", patternConstraint)).rejects.toBeTruthy();
  });

  it(`should resolve for valid input to ${ConstraintType[ConstraintType.IsInNumberRange]} constraint`, async () => {
    const lowerBoundConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Decimal,
      minValue: "12.3",
    };
    await expect(attributeSchema(t).validateAt("minValue", lowerBoundConstraint)).resolves.toBeTruthy();
    const upperBoundConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Decimal,
      maxValue: "-123",
    };
    await expect(attributeSchema(t).validateAt("maxValue", upperBoundConstraint)).resolves.toBeTruthy();
    const rangeConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Decimal,
      minValue: "12.3",
      maxValue: "56",
    };
    await expect(attributeSchema(t).validateAt("minValue", rangeConstraint)).resolves.toBeTruthy();
    await expect(attributeSchema(t).validateAt("maxValue", rangeConstraint)).resolves.toBeTruthy();
    const intRangeConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Integer,
      minValue: "12",
      maxValue: "56",
    };
    await expect(attributeSchema(t).validateAt("minValue", intRangeConstraint)).resolves.toBeTruthy();
    await expect(attributeSchema(t).validateAt("maxValue", intRangeConstraint)).resolves.toBeTruthy();
  });

  it(`should reject for invalid input to ${ConstraintType[ConstraintType.IsInNumberRange]} constraint`, async () => {
    const lowerBoundConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Decimal,
      minValue: "^12",
    };
    await expect(attributeSchema(t).validateAt("minValue", lowerBoundConstraint)).rejects.toBeTruthy();
    const upperBoundConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Integer,
      maxValue: "-1.23",
    };
    await expect(attributeSchema(t).validateAt("maxValue", upperBoundConstraint)).rejects.toBeTruthy();
    const emptyRangeConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Decimal,
      minValue: "",
      maxValue: "",
    };
    await expect(attributeSchema(t).validateAt("maxValue", emptyRangeConstraint)).rejects.toBeTruthy();
    const invalidRangeConstraint: Partial<AttributeFormFields> = {
      constraintType: ConstraintType.IsInNumberRange,
      dataType: XsdDataType.Integer,
      minValue: "100",
      maxValue: "56",
    };
    await expect(attributeSchema(t).validateAt("maxValue", invalidRangeConstraint)).rejects.toBeTruthy();
  });
});
