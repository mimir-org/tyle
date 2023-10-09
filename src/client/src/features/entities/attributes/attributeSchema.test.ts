import { attributeSchema } from "./attributeSchema";
import { AttributeFormFields } from "./types/formAttributeLib";
import { vi } from "vitest";

describe("attributeSchema tests", () => {
  const t = vi.fn();

  it("should reject without a name", async () => {
    const attributeWithoutName: Partial<AttributeFormFields> = { name: "" };
    await expect(attributeSchema(t).validateAt("name", attributeWithoutName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const attributeWithLongName: Partial<AttributeFormFields> = { name: "c".repeat(121) };
    await expect(attributeSchema(t).validateAt("name", attributeWithLongName)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const attributeWithLongDescription: Partial<AttributeFormFields> = { description: "c".repeat(501) };
    await expect(attributeSchema(t).validateAt("description", attributeWithLongDescription)).rejects.toBeTruthy();
  });
});
