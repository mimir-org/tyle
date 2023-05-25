import { UnitLibAm } from "@mimirorg/typelibrary-types";
import { unitSchema } from "./unitSchema";

describe("unitSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a name", async () => {
    const unitWithoutName: Partial<UnitLibAm> = { name: "" };
    await expect(unitSchema(t).validateAt("name", unitWithoutName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const unitWithLongName: Partial<UnitLibAm> = { name: "c".repeat(121) };
    await expect(unitSchema(t).validateAt("name", unitWithLongName)).rejects.toBeTruthy();
  });

  it("should reject with a symbol longer than 30 characters", async () => {
    const unitWithLongSymbol: Partial<UnitLibAm> = { symbol: "c".repeat(31) };
    await expect(unitSchema(t).validateAt("name", unitWithLongSymbol)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const unitWithLongDescription: Partial<UnitLibAm> = { description: "c".repeat(501) };
    await expect(unitSchema(t).validateAt("description", unitWithLongDescription)).rejects.toBeTruthy();
  });
});
