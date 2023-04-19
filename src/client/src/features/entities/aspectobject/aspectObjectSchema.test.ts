import { ConnectorDirection } from "@mimirorg/typelibrary-types";
import { aspectObjectSchema } from "features/entities/aspectobject/aspectObjectSchema";
import { FormAspectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";

describe("aspectObjectSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a name", async () => {
    const aspectObjectWithoutAName: Partial<FormAspectObjectLib> = { name: "" };
    await expect(aspectObjectSchema(t).validateAt("name", aspectObjectWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 60 characters", async () => {
    const aspectObjectWithLongName: Partial<FormAspectObjectLib> = { name: "c".repeat(61) };
    await expect(aspectObjectSchema(t).validateAt("name", aspectObjectWithLongName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS id", async () => {
    const aspectObjectWithoutRDSId: Partial<FormAspectObjectLib> = { rdsId: "" };
    await expect(aspectObjectSchema(t).validateAt("rdsCode", aspectObjectWithoutRDSId)).rejects.toBeTruthy();
  });

  it("should reject without a purpose name", async () => {
    const aspectObjectWithoutPurposeName: Partial<FormAspectObjectLib> = { purposeName: "" };
    await expect(aspectObjectSchema(t).validateAt("purposeName", aspectObjectWithoutPurposeName)).rejects.toBeTruthy();
  });

  it("should reject without an aspect", async () => {
    const aspectObjectWithoutAspect: Partial<FormAspectObjectLib> = { aspect: undefined };
    await expect(aspectObjectSchema(t).validateAt("aspect", aspectObjectWithoutAspect)).rejects.toBeTruthy();
  });

  it("should reject without an owner", async () => {
    const aspectObjectWithoutOwner: Partial<FormAspectObjectLib> = { companyId: 0 };
    await expect(aspectObjectSchema(t).validateAt("companyId", aspectObjectWithoutOwner)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const aspectObjectWithLongDescription: Partial<FormAspectObjectLib> = { description: "c".repeat(501) };
    await expect(aspectObjectSchema(t).validateAt("description", aspectObjectWithLongDescription)).rejects.toBeTruthy();
  });

  it("should reject if there are any terminals with a negative minQuantity", async () => {
    const aspectObjectWithNegativeTerminalMinQuantity: Partial<FormAspectObjectLib> = {
      aspectObjectTerminals: [
        {
          terminalId: "",
          hasMaxQuantity: false,
          minQuantity: -1,
          maxQuantity: 1,
          connectorDirection: ConnectorDirection.Input,
        },
      ],
    };

    await expect(
      aspectObjectSchema(t).validateAt("aspectObjectTerminals.minQuantity", aspectObjectWithNegativeTerminalMinQuantity)
    ).rejects.toBeTruthy();
  });

  it("should reject if there are any terminals with a negative maxQuantity", async () => {
    const aspectObjectWithNegativeTerminalMinQuantity: Partial<FormAspectObjectLib> = {
      aspectObjectTerminals: [
        {
          terminalId: "",
          hasMaxQuantity: true,
          minQuantity: 1,
          maxQuantity: -1,
          connectorDirection: ConnectorDirection.Input,
        },
      ],
    };

    await expect(
      aspectObjectSchema(t).validateAt("aspectObjectTerminals.maxQuantity", aspectObjectWithNegativeTerminalMinQuantity)
    ).rejects.toBeTruthy();
  });

  // TODO: When nullable ints are implemented this test can be uncommented.
  /*
  it("should reject if there are any terminals without an id", async () => {
    const aspectObjectWithEmptyTerminals: Partial<FormAspectObjectLib> = {
      aspectObjectTerminals: [
        {
          terminalId: null,
          minQuantity: 1,
          maxQuantity: 10,
          connectorDirection: ConnectorDirection.Input,
          hasMaxQuantity: true,
        },
      ],
    };
    await expect(aspectObjectSchema(t).validateAt("aspectObjectTerminals", aspectObjectWithEmptyTerminals)).rejects.toBeTruthy();
  });
  */
});
