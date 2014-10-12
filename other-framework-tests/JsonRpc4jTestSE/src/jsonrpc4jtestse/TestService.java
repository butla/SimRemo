package jsonrpc4jtestse;

public interface TestService
{
    String test();
    
    String test(String arg);
    
    DaneA testDataA(DaneA dane);
    
    Object[] testArray(Object[] array);
    
    int[] testArray2(int[] array);
}
