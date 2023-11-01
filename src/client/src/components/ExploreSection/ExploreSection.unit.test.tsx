import { MimirorgThemeProvider } from "@mimirorg/component-library";
import "@testing-library/jest-dom";
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect } from "vitest";
import ExploreSection from "./ExploreSection";

const setup = () => {
  const testComponent = render(
    <MimirorgThemeProvider theme={"tyleLight"}>
      <ExploreSection title={"test"}>
        <></>
      </ExploreSection>
    </MimirorgThemeProvider>,
  );
  const explorerHeader = screen.getByText("test");

  return {
    explorerHeader,
    ...testComponent,
  };
};
describe("Explorer section unit test", () => {
  afterEach(() => {
    cleanup();
  });
  test("Did component render", () => {
    const { explorerHeader } = setup();
    expect(explorerHeader).toBeInTheDocument();
  });
});
