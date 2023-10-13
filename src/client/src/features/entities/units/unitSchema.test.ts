import { UnitLibAm } from "@mimirorg/typelibrary-types";
import { unitSchema } from "./unitSchema";

describe("unitSchema tests", () => {
  it("should reject without a name", async () => {
    const unitWithoutName: Partial<UnitLibAm> = { name: "" };
    await expect(unitSchema.validateAt("name", unitWithoutName)).rejects.toBeTruthy();
  });
});
