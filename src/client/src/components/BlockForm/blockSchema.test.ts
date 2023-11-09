import { DESCRIPTION_LENGTH, NAME_LENGTH, NOTATION_LENGTH } from "types/common/stringLengthConstants";
import { vi } from "vitest";
import { BlockFormFields } from "./BlockForm.helpers";
import { blockSchema } from "./blockSchema";

describe("blockSchema tests", () => {
  const t = vi.fn();

  it("should resolve with a name", async () => {
    const blockWithName: Partial<BlockFormFields> = { name: "Test name" };
    await expect(blockSchema(t).validateAt("name", blockWithName)).resolves.toBeTruthy();
  });

  it("should reject without a name", async () => {
    const blockWithoutName: Partial<BlockFormFields> = { name: "" };
    await expect(blockSchema(t).validateAt("name", blockWithoutName)).rejects.toBeTruthy();
  });

  it("should resolve with a name with the limit length", async () => {
    const blockWithLongName: Partial<BlockFormFields> = { name: "c".repeat(NAME_LENGTH) };
    await expect(blockSchema(t).validateAt("name", blockWithLongName)).resolves.toBeTruthy();
  });

  it("should reject with a name longer than the limit", async () => {
    const blockWithLongName: Partial<BlockFormFields> = { name: "c".repeat(NAME_LENGTH + 1) };
    await expect(blockSchema(t).validateAt("name", blockWithLongName)).rejects.toBeTruthy();
  });

  it("should resolve with a description with the limit length", async () => {
    const blockWithLongDescription: Partial<BlockFormFields> = {
      description: "c".repeat(DESCRIPTION_LENGTH),
    };
    await expect(blockSchema(t).validateAt("description", blockWithLongDescription)).resolves.toBeTruthy();
  });

  it("should reject with a description longer than the limit", async () => {
    const blockWithLongDescription: Partial<BlockFormFields> = {
      description: "c".repeat(DESCRIPTION_LENGTH + 1),
    };
    await expect(blockSchema(t).validateAt("description", blockWithLongDescription)).rejects.toBeTruthy();
  });

  it("should resolve with a notation with the limit length", async () => {
    const blockWithLongNotation: Partial<BlockFormFields> = {
      notation: "c".repeat(NOTATION_LENGTH),
    };
    await expect(blockSchema(t).validateAt("notation", blockWithLongNotation)).resolves.toBeTruthy();
  });

  it("should reject with a notation longer than the limit", async () => {
    const blockWithLongNotation: Partial<BlockFormFields> = {
      notation: "c".repeat(NOTATION_LENGTH + 1),
    };
    await expect(blockSchema(t).validateAt("notation", blockWithLongNotation)).rejects.toBeTruthy();
  });
});
