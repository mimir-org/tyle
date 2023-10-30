import { blockSchema } from "features/entities/block/blockSchema";

import { vi } from "vitest";
import { BlockFormFields } from "./BlockForm.helpers";

describe("blockSchema tests", () => {
  const t = vi.fn();

  it("should reject without a name", async () => {
    const blockWithoutAName: Partial<BlockFormFields> = { name: "" };
    await expect(blockSchema(t).validateAt("name", blockWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const blockWithLongName: Partial<BlockFormFields> = { name: "c".repeat(121) };
    await expect(blockSchema(t).validateAt("name", blockWithLongName)).rejects.toBeTruthy();
  });

  // it("should reject without a purpose name", async () => {
  //   const blockWithoutPurposeName: Partial<BlockFormFields> = { purposeName: "" };
  //   await expect(blockSchema(t).validateAt("purposeName", blockWithoutPurposeName)).rejects.toBeTruthy();
  // });

  // it("should reject without an aspect", async () => {
  //   const blockWithoutAspect: Partial<BlockFormFields> = { aspect: undefined };
  //   await expect(blockSchema(t).validateAt("aspect", blockWithoutAspect)).rejects.toBeTruthy();
  // });

  it("should reject with a description longer than 500 characters", async () => {
    const blockWithLongDescription: Partial<BlockFormFields> = { description: "c".repeat(501) };
    await expect(blockSchema(t).validateAt("description", blockWithLongDescription)).rejects.toBeTruthy();
  });

  // it("should reject if there are any terminals with a negative minQuantity", async () => {
  //   const blockWithNegativeTerminalMinQuantity: Partial<BlockFormFields> = {
  //     blockTerminals: [
  //       {
  //         terminalId: "",
  //         hasMaxQuantity: false,
  //         minQuantity: -1,
  //         maxQuantity: 1,
  //         connectorDirection: ConnectorDirection.Input,
  //       },
  //     ],
  //   };

  //   await expect(
  //     blockSchema(t).validateAt("blockTerminals.minQuantity", blockWithNegativeTerminalMinQuantity),
  //   ).rejects.toBeTruthy();
  // });

  // it("should reject if there are any terminals with a negative maxQuantity", async () => {
  //   const blockWithNegativeTerminalMinQuantity: Partial<BlockFormFields> = {
  //     blockTerminals: [
  //       {
  //         terminalId: "",
  //         hasMaxQuantity: true,
  //         minQuantity: 1,
  //         maxQuantity: -1,
  //         connectorDirection: ConnectorDirection.Input,
  //       },
  //     ],
  //   };

  //   await expect(
  //     blockSchema(t).validateAt("blockTerminals.maxQuantity", blockWithNegativeTerminalMinQuantity),
  //   ).rejects.toBeTruthy();
  // });

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
