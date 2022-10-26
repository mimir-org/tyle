import { interfaceSchema } from "./interfaceSchema";
import { FormInterfaceLib } from "./types/formInterfaceLib";

describe("interfaceSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a name", async () => {
    const interfaceWithoutAName: Partial<FormInterfaceLib> = { name: "" };
    await expect(interfaceSchema(t).validateAt("name", interfaceWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 60 characters", async () => {
    const interfaceWithLongName: Partial<FormInterfaceLib> = { name: "c".repeat(61) };
    await expect(interfaceSchema(t).validateAt("name", interfaceWithLongName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS name", async () => {
    const interfaceWithoutRDSName: Partial<FormInterfaceLib> = { rdsName: "" };
    await expect(interfaceSchema(t).validateAt("rdsName", interfaceWithoutRDSName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS code", async () => {
    const interfaceWithoutRDSCode: Partial<FormInterfaceLib> = { rdsCode: "" };
    await expect(interfaceSchema(t).validateAt("rdsCode", interfaceWithoutRDSCode)).rejects.toBeTruthy();
  });

  it("should reject without a purpose name", async () => {
    const interfaceWithoutPurposeName: Partial<FormInterfaceLib> = { purposeName: "" };
    await expect(interfaceSchema(t).validateAt("purposeName", interfaceWithoutPurposeName)).rejects.toBeTruthy();
  });

  it("should reject without an aspect", async () => {
    const interfaceWithoutAspect: Partial<FormInterfaceLib> = { aspect: undefined };
    await expect(interfaceSchema(t).validateAt("aspect", interfaceWithoutAspect)).rejects.toBeTruthy();
  });

  it("should reject without an owner", async () => {
    const interfaceWithoutOwner: Partial<FormInterfaceLib> = { companyId: 0 };
    await expect(interfaceSchema(t).validateAt("companyId", interfaceWithoutOwner)).rejects.toBeTruthy();
  });

  it("should reject without a terminal", async () => {
    const interfaceWithoutTerminal: Partial<FormInterfaceLib> = { terminalId: "" };
    await expect(interfaceSchema(t).validateAt("companyId", interfaceWithoutTerminal)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const interfaceWithLongDescription: Partial<FormInterfaceLib> = { description: "c".repeat(501) };
    await expect(interfaceSchema(t).validateAt("description", interfaceWithLongDescription)).rejects.toBeTruthy();
  });
});
