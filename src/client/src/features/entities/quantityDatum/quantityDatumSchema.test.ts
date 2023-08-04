import { QuantityDatumLibAm } from "@mimirorg/typelibrary-types";
import { quantityDatumSchema } from "./quantityDatumSchema";

describe("quantityDatumSchema tests", () => {
  const t = jest.fn();

  it("should reject without a name", async () => {
    const quantityDatumWithoutName: Partial<QuantityDatumLibAm> = { name: "" };
    await expect(quantityDatumSchema(t).validateAt("name", quantityDatumWithoutName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const quantityDatumWithLongName: Partial<QuantityDatumLibAm> = { name: "c".repeat(121) };
    await expect(quantityDatumSchema(t).validateAt("name", quantityDatumWithLongName)).rejects.toBeTruthy();
  });

  it("should reject without a type", async () => {
    const quantityDatumWithoutType: Partial<QuantityDatumLibAm> = { quantityDatumType: undefined };
    await expect(quantityDatumSchema(t).validateAt("name", quantityDatumWithoutType)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const quantityDatumWithLongDescription: Partial<QuantityDatumLibAm> = { description: "c".repeat(501) };
    await expect(
      quantityDatumSchema(t).validateAt("description", quantityDatumWithLongDescription),
    ).rejects.toBeTruthy();
  });
});
