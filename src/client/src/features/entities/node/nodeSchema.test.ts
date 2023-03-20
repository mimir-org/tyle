import { ConnectorDirection } from "@mimirorg/typelibrary-types";
import { nodeSchema } from "features/entities/node/nodeSchema";
import { FormNodeLib } from "features/entities/node/types/formNodeLib";

describe("nodeSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a name", async () => {
    const nodeWithoutAName: Partial<FormNodeLib> = { name: "" };
    await expect(nodeSchema(t).validateAt("name", nodeWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 60 characters", async () => {
    const nodeWithLongName: Partial<FormNodeLib> = { name: "c".repeat(61) };
    await expect(nodeSchema(t).validateAt("name", nodeWithLongName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS name", async () => {
    const nodeWithoutRDSName: Partial<FormNodeLib> = { rdsName: "" };
    await expect(nodeSchema(t).validateAt("rdsName", nodeWithoutRDSName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS code", async () => {
    const nodeWithoutRDSCode: Partial<FormNodeLib> = { rdsCode: "" };
    await expect(nodeSchema(t).validateAt("rdsCode", nodeWithoutRDSCode)).rejects.toBeTruthy();
  });

  it("should reject without a purpose name", async () => {
    const nodeWithoutPurposeName: Partial<FormNodeLib> = { purposeName: "" };
    await expect(nodeSchema(t).validateAt("purposeName", nodeWithoutPurposeName)).rejects.toBeTruthy();
  });

  it("should reject without an aspect", async () => {
    const nodeWithoutAspect: Partial<FormNodeLib> = { aspect: undefined };
    await expect(nodeSchema(t).validateAt("aspect", nodeWithoutAspect)).rejects.toBeTruthy();
  });

  it("should reject without an owner", async () => {
    const nodeWithoutOwner: Partial<FormNodeLib> = { companyId: 0 };
    await expect(nodeSchema(t).validateAt("companyId", nodeWithoutOwner)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const nodeWithLongDescription: Partial<FormNodeLib> = { description: "c".repeat(501) };
    await expect(nodeSchema(t).validateAt("description", nodeWithLongDescription)).rejects.toBeTruthy();
  });

  it("should reject if there are any terminals with a negative minQuantity", async () => {
    const nodeWithNegativeTerminalMinQuantity: Partial<FormNodeLib> = {
      nodeTerminals: [
        {
          terminalId: 0,
          hasMaxQuantity: false,
          minQuantity: -1,
          maxQuantity: 1,
          connectorDirection: ConnectorDirection.Input,
        },
      ],
    };

    await expect(
      nodeSchema(t).validateAt("nodeTerminals.minQuantity", nodeWithNegativeTerminalMinQuantity)
    ).rejects.toBeTruthy();
  });

  it("should reject if there are any terminals with a negative maxQuantity", async () => {
    const nodeWithNegativeTerminalMinQuantity: Partial<FormNodeLib> = {
      nodeTerminals: [
        {
          terminalId: 0,
          hasMaxQuantity: true,
          minQuantity: 1,
          maxQuantity: -1,
          connectorDirection: ConnectorDirection.Input,
        },
      ],
    };

    await expect(
      nodeSchema(t).validateAt("nodeTerminals.maxQuantity", nodeWithNegativeTerminalMinQuantity)
    ).rejects.toBeTruthy();
  });

  // TODO: When nullable ints are implemented this test can be uncommented.
  /*
  it("should reject if there are any terminals without an id", async () => {
    const nodeWithEmptyTerminals: Partial<FormNodeLib> = {
      nodeTerminals: [
        {
          terminalId: null,
          minQuantity: 1,
          maxQuantity: 10,
          connectorDirection: ConnectorDirection.Input,
          hasMaxQuantity: true,
        },
      ],
    };
    await expect(nodeSchema(t).validateAt("nodeTerminals", nodeWithEmptyTerminals)).rejects.toBeTruthy();
  });
  */
});
