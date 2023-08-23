import { ConnectorDirection } from "@mimirorg/typelibrary-types";
import { blockSchema } from "features/entities/block/blockSchema";
import { FormBlockLib } from "features/entities/block/types/formBlockLib";
import { vi } from "vitest";

describe("blockSchema tests", () => {
  const t = vi.fn();

  it("should reject without a name", async () => {
    const blockWithoutAName: Partial<FormBlockLib> = { name: "" };
    await expect(blockSchema(t).validateAt("name", blockWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const blockWithLongName: Partial<FormBlockLib> = { name: "c".repeat(121) };
    await expect(blockSchema(t).validateAt("name", blockWithLongName)).rejects.toBeTruthy();
  });

  it("should reject without a RDS id", async () => {
    const blockWithoutRDSId: Partial<FormBlockLib> = { rdsId: "" };
    await expect(blockSchema(t).validateAt("rdsId", blockWithoutRDSId)).rejects.toBeTruthy();
  });

  it("should reject without a purpose name", async () => {
    const blockWithoutPurposeName: Partial<FormBlockLib> = { purposeName: "" };
    await expect(blockSchema(t).validateAt("purposeName", blockWithoutPurposeName)).rejects.toBeTruthy();
  });

  it("should reject without an aspect", async () => {
    const blockWithoutAspect: Partial<FormBlockLib> = { aspect: undefined };
    await expect(blockSchema(t).validateAt("aspect", blockWithoutAspect)).rejects.toBeTruthy();
  });

  it("should reject without an owner", async () => {
    const blockWithoutOwner: Partial<FormBlockLib> = { companyId: 0 };
    await expect(blockSchema(t).validateAt("companyId", blockWithoutOwner)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const blockWithLongDescription: Partial<FormBlockLib> = { description: "c".repeat(501) };
    await expect(blockSchema(t).validateAt("description", blockWithLongDescription)).rejects.toBeTruthy();
  });

  it("should reject if there are any terminals with a negative minQuantity", async () => {
    const blockWithNegativeTerminalMinQuantity: Partial<FormBlockLib> = {
      blockTerminals: [
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
      blockSchema(t).validateAt(
        "blockTerminals.minQuantity",
        blockWithNegativeTerminalMinQuantity,
      ),
    ).rejects.toBeTruthy();
  });

  it("should reject if there are any terminals with a negative maxQuantity", async () => {
    const blockWithNegativeTerminalMinQuantity: Partial<FormBlockLib> = {
      blockTerminals: [
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
      blockSchema(t).validateAt(
        "blockTerminals.maxQuantity",
        blockWithNegativeTerminalMinQuantity,
      ),
    ).rejects.toBeTruthy();
  });

  // TODO: When nullable ints are implemented this test can be uncommented.
  /*
  it("should reject if there are any terminals without an id", async () => {
    const blockWithEmptyTerminals: Partial<FormBlockLib> = {
      blockTerminals: [
        {
          terminalId: null,
          minQuantity: 1,
          maxQuantity: 10,
          connectorDirection: ConnectorDirection.Input,
          hasMaxQuantity: true,
        },
      ],
    };
    await expect(blockSchema(t).validateAt("blockTerminals", blockWithEmptyTerminals)).rejects.toBeTruthy();
  });
  */
});
