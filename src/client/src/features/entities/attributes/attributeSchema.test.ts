import { DESCRIPTION_LENGTH, NAME_LENGTH } from "common/types/common/stringLengthConstants";
import { AttributeFormFields } from "./AttributeForm.helpers";
import { attributeSchema } from "./attributeSchema";
import { vi } from "vitest";

describe("attributeSchema tests", () => {
  const t = vi.fn();

  it("should reject without a name", async () => {
    const attributeWithoutName: Partial<AttributeFormFields> = { name: "" };
    await expect(attributeSchema(t).validateAt("name", attributeWithoutName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than the limit", async () => {
    const attributeWithLongName: Partial<AttributeFormFields> = { name: "c".repeat(NAME_LENGTH + 1) };
    await expect(attributeSchema(t).validateAt("name", attributeWithLongName)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than the limit", async () => {
    const attributeWithLongDescription: Partial<AttributeFormFields> = {
      description: "c".repeat(DESCRIPTION_LENGTH + 1),
    };
    await expect(attributeSchema(t).validateAt("description", attributeWithLongDescription)).rejects.toBeTruthy();
  });
});
