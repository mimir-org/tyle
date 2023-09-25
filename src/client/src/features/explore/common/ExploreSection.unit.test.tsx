import { afterEach, describe, expect } from "vitest";
import { ExploreSection } from "./ExploreSection";
import { cleanup, render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import { MimirorgThemeProvider } from "@mimirorg/component-library";
import React from "react";

const setup = () => {
  const testComponent = render(
    <MimirorgThemeProvider theme={"tyleLight"}>
      <ExploreSection title={"test"} children={<></>} />
    </MimirorgThemeProvider>
  );
  const explorerHeader = screen.getByText("test");

  return {
    explorerHeader,
    ...testComponent
  };
};
describe("Explorer section unit test", () => {
  afterEach(() => {
    cleanup();
  });
  test("Did component render", () => {
    const { explorerHeader } = setup();
    expect(explorerHeader).toBeInTheDocument();
  })
  ;
})
;