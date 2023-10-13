import { terminalSchema } from "features/entities/terminal/terminalSchema";
import { TerminalFormFields } from "./TerminalForm.helpers";

describe("terminalSchema tests", () => {
  it("should reject without a name", async () => {
    const terminalWithoutAName: Partial<TerminalFormFields> = { name: "" };
    await expect(terminalSchema().validateAt("name", terminalWithoutAName)).rejects.toBeTruthy();
  });
});
