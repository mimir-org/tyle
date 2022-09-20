import { Select } from "@mimirorg/typelibrary-types";
import { attributeSchema } from "../../content/forms/attribute/attributeSchema";
import { FormAttributeLib } from "../../content/forms/attribute/types/formAttributeLib";
import { FormTransportLib } from "../../content/forms/transport/types/formTransportLib";

describe("attributeSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a name", async () => {
    const attributeWithoutAName: Partial<FormAttributeLib> = { name: "" };
    await expect(attributeSchema(t).validateAt("name", attributeWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 30 characters", async () => {
    const attributeWithoutLongName: Partial<FormTransportLib> = { name: "c".repeat(31) };
    await expect(attributeSchema(t).validateAt("name", attributeWithoutLongName)).rejects.toBeTruthy();
  });

  it("should reject without an aspect", async () => {
    const attributeWithoutAspect: Partial<FormAttributeLib> = { aspect: undefined };
    await expect(attributeSchema(t).validateAt("aspect", attributeWithoutAspect)).rejects.toBeTruthy();
  });

  it("should reject without a discipline", async () => {
    const attributeWithoutDiscipline: Partial<FormAttributeLib> = { discipline: undefined };
    await expect(attributeSchema(t).validateAt("discipline", attributeWithoutDiscipline)).rejects.toBeTruthy();
  });

  it("should reject without an owner", async () => {
    const attributeWithoutOwner: Partial<FormAttributeLib> = { companyId: 0 };
    await expect(attributeSchema(t).validateAt("companyId", attributeWithoutOwner)).rejects.toBeTruthy();
  });

  it("should reject if select does not equal none and selectValues array is empty", async () => {
    const attributeWithEmptySelectValues: Partial<FormAttributeLib> = {
      select: Select.SingleSelect,
      selectValues: [],
    };
    await expect(attributeSchema(t).validateAt("selectValues", attributeWithEmptySelectValues)).rejects.toBeTruthy();
  });

  it("should reject if there are any empty units", async () => {
    const attributeWithEmptyUnits: Partial<FormAttributeLib> = {
      unitIdList: [{ value: "" }],
    };
    await expect(attributeSchema(t).validateAt("unitIdList", attributeWithEmptyUnits)).rejects.toBeTruthy();
  });
});
