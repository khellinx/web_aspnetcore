using Digipolis.Web.Api;
using Digipolis.Web.Api.ApiExplorer;
using Digipolis.Web.UnitTests._TestFixtures;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Digipolis.Web.UnitTests.Api.ApiExplorer
{
    // TODO: Tijdelijk uitgeschakeld. De fixture geeft nog een NullReferenceException bij het uitvoeren van de DefaultApiDescriptionProvider op de context.
    //public class LowerCaseQueryParametersApiDescriptionProviderTests : IClassFixture<ApiDescriptionProviderContextCrudControllerFixture>
    //{
        //public LowerCaseQueryParametersApiDescriptionProviderTests(ApiDescriptionProviderContextCrudControllerFixture fixture)
        //{
        //    Context = fixture.CreateContext();
        //}

        //public ApiDescriptionProviderContext Context { get; private set; }
        //
        //[Fact]
        //public void OnProvidersExecutingSetsQueryParameterNamesToLowerCase()
        //{
        //    // Arrange
        //    var upperCaseChars = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        //    var provider = new LowerCaseQueryParametersApiDescriptionProvider(null);
        //    var queryParams = Context.Results.SelectMany(act => act.ParameterDescriptions).Where(param => param.Source.Id == "Query");

        //    // Precondition assert: At least one of the query parameters should contain an uppercase character.
        //    Assert.True(queryParams.SelectMany(x => x.Name).Any(x => upperCaseChars.Contains(x)));

        //    // Act
        //    provider.OnProvidersExecuting(Context);

        //    // Assert
        //    // None of the query parameters may contain an uppercase character
        //    Assert.False(queryParams.SelectMany(x => x.Name).Any(x => upperCaseChars.Contains(x)));
        //}
    //}
}
