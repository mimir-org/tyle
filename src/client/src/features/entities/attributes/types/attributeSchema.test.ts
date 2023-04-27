import { AttributeSchema, attributeSchema } from "../attributeSchema";
describe("attributeSchema tests", () => {
  it("should reject without a name", async () => {
    const attributeWithoutName: Partial<AttributeSchema> = { name: "" };
    await expect(attributeSchema.validateAt("name", attributeWithoutName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 60 characters", async () => {
    const attributeWithLongName: Partial<AttributeSchema> = { name: "c".repeat(61) };
    await expect(attributeSchema.validateAt("name", attributeWithLongName)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const terminalWithLongDescription: Partial<AttributeSchema> = { description: "c".repeat(501) };
    await expect(attributeSchema.validateAt("description", terminalWithLongDescription)).rejects.toBeTruthy();
  });
});