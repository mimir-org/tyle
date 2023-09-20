import { afterEach, describe, expect } from "vitest";
import { ExploreSection } from "./ExploreSection";
import { cleanup, render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import { MimirorgThemeProvider } from "@mimirorg/component-library";

//TODO: Fikse eller fjerne denne testen.
describe("Theme is the root to all evil", () => {
  test("prove that theme is the root cause to all evil", () => {
    render(
      <MimirorgThemeProvider theme={"tyleLight"}>
        <ExploreSection title={"test"} children={<></>} />
      </MimirorgThemeProvider>
    );

    const element = screen.getByText("test");
    expect(element).toBeInTheDocument();

    afterEach(() => {
      cleanup();
    });
  })
  ;
})
;