import { RdsLibAm } from "@mimirorg/typelibrary-types";
import { rdsSchema } from "./rdsSchema";
import { vi } from "vitest";

describe("rdsSchema tests", () => {
  const t = vi.fn();

  it("should reject without a code", async () => {
    const rdsWithoutCode: Partial<RdsLibAm> = { rdsCode: "" };
    await expect(rdsSchema(t, []).validateAt("name", rdsWithoutCode)).rejects.toBeTruthy();
  });

  it("should reject with a code longer than 10 characters", async () => {
    const rdsWithLongCode: Partial<RdsLibAm> = { rdsCode: "c".repeat(11) };
    await expect(rdsSchema(t, []).validateAt("name", rdsWithLongCode)).rejects.toBeTruthy();
  });

  it("should reject with a code that is in use", async () => {
    const rdsWithLongCode: Partial<RdsLibAm> = { rdsCode: "A" };
    await expect(rdsSchema(t, ["A", "B", "C"]).validateAt("name", rdsWithLongCode)).rejects.toBeTruthy();
  });

  it("should reject without a name", async () => {
    const rdsWithoutName: Partial<RdsLibAm> = { name: "" };
    await expect(rdsSchema(t, []).validateAt("name", rdsWithoutName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const rdsWithLongName: Partial<RdsLibAm> = { name: "c".repeat(121) };
    await expect(rdsSchema(t, []).validateAt("name", rdsWithLongName)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const rdsWithLongDescription: Partial<RdsLibAm> = { description: "c".repeat(501) };
    await expect(rdsSchema(t, []).validateAt("description", rdsWithLongDescription)).rejects.toBeTruthy();
  });
});
