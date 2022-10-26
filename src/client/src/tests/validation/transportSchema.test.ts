import { transportSchema } from "../../features/entities/transport/transportSchema";
import { FormTransportLib } from "../../features/entities/transport/types/formTransportLib";

describe("transportSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a name", async () => {
    const transportWithoutAName: Partial<FormTransportLib> = { name: "" };
    await expect(transportSchema(t).validateAt("name", transportWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 60 characters", async () => {
    const transportWithLongName: Partial<FormTransportLib> = { name: "c".repeat(61) };
    await expect(transportSchema(t).validateAt("name", transportWithLongName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS name", async () => {
    const transportWithoutRDSName: Partial<FormTransportLib> = { rdsName: "" };
    await expect(transportSchema(t).validateAt("rdsName", transportWithoutRDSName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS code", async () => {
    const transportWithoutRDSCode: Partial<FormTransportLib> = { rdsCode: "" };
    await expect(transportSchema(t).validateAt("rdsCode", transportWithoutRDSCode)).rejects.toBeTruthy();
  });

  it("should reject without a purpose name", async () => {
    const transportWithoutPurposeName: Partial<FormTransportLib> = { purposeName: "" };
    await expect(transportSchema(t).validateAt("purposeName", transportWithoutPurposeName)).rejects.toBeTruthy();
  });

  it("should reject without an aspect", async () => {
    const transportWithoutAspect: Partial<FormTransportLib> = { aspect: undefined };
    await expect(transportSchema(t).validateAt("aspect", transportWithoutAspect)).rejects.toBeTruthy();
  });

  it("should reject without an owner", async () => {
    const transportWithoutOwner: Partial<FormTransportLib> = { companyId: 0 };
    await expect(transportSchema(t).validateAt("companyId", transportWithoutOwner)).rejects.toBeTruthy();
  });

  it("should reject without a terminal", async () => {
    const transportWithoutTerminal: Partial<FormTransportLib> = { terminalId: "" };
    await expect(transportSchema(t).validateAt("companyId", transportWithoutTerminal)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const transportWithLongDescription: Partial<FormTransportLib> = { description: "c".repeat(501) };
    await expect(transportSchema(t).validateAt("description", transportWithLongDescription)).rejects.toBeTruthy();
  });
});
