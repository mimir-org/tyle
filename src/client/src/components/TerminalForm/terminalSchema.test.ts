import { DESCRIPTION_LENGTH, IRI_LENGTH, NAME_LENGTH, NOTATION_LENGTH } from "types/common/stringLengthConstants";
import { Direction } from "types/terminals/direction";
import { vi } from "vitest";
import { TerminalFormFields } from "./TerminalForm.helpers";
import { terminalSchema } from "./terminalSchema";

describe("terminalSchema tests", () => {
  const t = vi.fn();

  it("should resolve with a name", async () => {
    const terminalWithoutName: Partial<TerminalFormFields> = { name: "Test name" };
    await expect(terminalSchema(t).validateAt("name", terminalWithoutName)).resolves.toBeTruthy();
  });

  it("should reject without a name", async () => {
    const terminalWithoutName: Partial<TerminalFormFields> = { name: "" };
    await expect(terminalSchema(t).validateAt("name", terminalWithoutName)).rejects.toBeTruthy();
  });

  it("should resolve with a name with the limit length", async () => {
    const terminalWithLongName: Partial<TerminalFormFields> = { name: "c".repeat(NAME_LENGTH) };
    await expect(terminalSchema(t).validateAt("name", terminalWithLongName)).resolves.toBeTruthy();
  });

  it("should reject with a name longer than the limit", async () => {
    const terminalWithLongName: Partial<TerminalFormFields> = { name: "c".repeat(NAME_LENGTH + 1) };
    await expect(terminalSchema(t).validateAt("name", terminalWithLongName)).rejects.toBeTruthy();
  });

  it("should resolve with a description with the limit length", async () => {
    const terminalWithLongDescription: Partial<TerminalFormFields> = {
      description: "c".repeat(DESCRIPTION_LENGTH),
    };
    await expect(terminalSchema(t).validateAt("description", terminalWithLongDescription)).resolves.toBeTruthy();
  });

  it("should reject with a description longer than the limit", async () => {
    const terminalWithLongDescription: Partial<TerminalFormFields> = {
      description: "c".repeat(DESCRIPTION_LENGTH + 1),
    };
    await expect(terminalSchema(t).validateAt("description", terminalWithLongDescription)).rejects.toBeTruthy();
  });

  it("should resolve with classifiers array", async () => {
    const terminalWithClassifiersArray: Partial<TerminalFormFields> = {
      classifiers: [],
    };
    await expect(terminalSchema(t).validateAt("classifiers", terminalWithClassifiersArray)).resolves.toBeTruthy();
  });

  it("should reject without classifiers array", async () => {
    const terminalWithoutClassifiersArray: Partial<TerminalFormFields> = {};
    await expect(terminalSchema(t).validateAt("classifiers", terminalWithoutClassifiersArray)).rejects.toBeTruthy();
  });

  it("should resolve with a notation with the limit length", async () => {
    const terminalWithLongNotation: Partial<TerminalFormFields> = {
      notation: "c".repeat(NOTATION_LENGTH),
    };
    await expect(terminalSchema(t).validateAt("notation", terminalWithLongNotation)).resolves.toBeTruthy();
  });

  it("should reject with a notation longer than the limit", async () => {
    const terminalWithLongNotation: Partial<TerminalFormFields> = {
      notation: "c".repeat(NOTATION_LENGTH + 1),
    };
    await expect(terminalSchema(t).validateAt("notation", terminalWithLongNotation)).rejects.toBeTruthy();
  });

  it("should resolve with a symbol with the limit length", async () => {
    const terminalWithLongSymbol: Partial<TerminalFormFields> = {
      symbol: "c".repeat(IRI_LENGTH),
    };
    await expect(terminalSchema(t).validateAt("symbol", terminalWithLongSymbol)).resolves.toBeTruthy();
  });

  it("should reject with a symbol longer than the limit", async () => {
    const terminalWithLongSymbol: Partial<TerminalFormFields> = {
      symbol: "c".repeat(IRI_LENGTH + 1),
    };
    await expect(terminalSchema(t).validateAt("symbol", terminalWithLongSymbol)).rejects.toBeTruthy();
  });

  it("should resolve with qualifier", async () => {
    const terminalWithQualifier: Partial<TerminalFormFields> = {
      qualifier: Direction.Output,
    };
    await expect(terminalSchema(t).validateAt("qualifier", terminalWithQualifier)).resolves.toBeTruthy();
  });

  it("should reject without qualifier", async () => {
    const terminalWithQualifier: Partial<TerminalFormFields> = {};
    await expect(terminalSchema(t).validateAt("qualifier", terminalWithQualifier)).rejects.toBeTruthy();
  });

  it("should resolve with attributes array", async () => {
    const terminalWithAttributesArray: Partial<TerminalFormFields> = {
      attributes: [],
    };
    await expect(terminalSchema(t).validateAt("attributes", terminalWithAttributesArray)).resolves.toBeTruthy();
  });

  it("should reject without attributes array", async () => {
    const terminalWithoutAttributesArray: Partial<TerminalFormFields> = {};
    await expect(terminalSchema(t).validateAt("attributes", terminalWithoutAttributesArray)).rejects.toBeTruthy();
  });
});
