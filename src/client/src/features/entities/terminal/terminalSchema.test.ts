import { terminalSchema } from "features/entities/terminal/terminalSchema";
import { FormTerminalLib } from "features/entities/terminal/types/formTerminalLib";

describe("terminalSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a name", async () => {
    const terminalWithoutAName: Partial<FormTerminalLib> = { name: "" };
    await expect(terminalSchema(t).validateAt("name", terminalWithoutAName)).rejects.toBeTruthy();
  });

  it("should reject with a name longer than 120 characters", async () => {
    const terminalWithLongName: Partial<FormTerminalLib> = { name: "c".repeat(121) };
    await expect(terminalSchema(t).validateAt("name", terminalWithLongName)).rejects.toBeTruthy();
  });

  it("should reject without a color", async () => {
    const terminalWithoutColor: Partial<FormTerminalLib> = { color: "" };
    await expect(terminalSchema(t).validateAt("name", terminalWithoutColor)).rejects.toBeTruthy();
  });

  it("should reject with a description longer than 500 characters", async () => {
    const terminalWithLongDescription: Partial<FormTerminalLib> = { description: "c".repeat(501) };
    await expect(terminalSchema(t).validateAt("description", terminalWithLongDescription)).rejects.toBeTruthy();
  });
});
