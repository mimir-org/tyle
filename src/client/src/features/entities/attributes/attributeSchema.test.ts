import { attributeSchema } from "./attributeSchema";
import { FormAttributeLib } from "./types/formAttributeLib";

describe("attributeSchema tests", () => {
  const t = jest.fn();

  it("should reject without a name", async () => {
    const attributeWithoutName: Partial<FormAttributeLib> = { name: "" };
    await expect(attributeSchema(t).validateAt("name", attributeWithoutName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const attributeWithLongName: Partial<FormAttributeLib> = { name: "c".repeat(121) };
    await expect(attributeSchema(t).validateAt("name", attributeWithLongName)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const attributeWithLongDescription: Partial<FormAttributeLib> = { description: "c".repeat(501) };
    await expect(attributeSchema(t).validateAt("description", attributeWithLongDescription)).rejects.toBeTruthy();
  });
});
